using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Text.Json.Nodes;
using health_path.Model;
using health_path.Util;

namespace health_path.Controllers;
    
internal record ProductBasicRecord(string Id, string LicenceNo, string ProductName, string CompanyName, bool Active);

[ApiController]
[Route("api/[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    private readonly HttpClient _httpClient;
    private readonly string _lnhpdBaseUrl;
    private readonly IDbConnection _connection;

    public CatalogController(ILogger<CatalogController> logger, HttpClient httpClient, IConfiguration config, IDbConnection connection)
    {
        _logger = logger;
        _httpClient = httpClient;
        var lnhpdBaseUrl = config.GetValue<string>("LnhpdBaseUrl");
        if (lnhpdBaseUrl == null) {
            throw new ArgumentNullException("Config item LnhpdBaseUrl is required");
        } else {
            _lnhpdBaseUrl = lnhpdBaseUrl;
        }
        _connection = connection;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<NaturalProduct>>> Fetch()
    {
        var nhpids = GetStockedProducts();
        var productRecords = await FetchProductRecords(nhpids);
        var ids = productRecords.Keys;
        var purposes = await FetchProductPurposes(ids);

        var products = new List<NaturalProduct>();
        foreach (var productRecord in productRecords.Values) {
            products.Add(new NaturalProduct(
                productRecord.LicenceNo,
                productRecord.ProductName,
                productRecord.CompanyName,
                productRecord.Active,
                purposes.GetValueOrDefault(productRecord.Id, ImmutableList<string>.Empty)
            ));
        }

        return Ok(products);
    }

    private IEnumerable<string> GetStockedProducts()
    {
        return _connection.Query<string>(@"
            SELECT LicenceNo
            FROM StockedProducts
            ORDER BY LicenceNo
            OFFSET 200 ROWS FETCH NEXT 100 ROWS ONLY
        ");
    }

    private async Task<ImmutableDictionary<string, ProductBasicRecord>> FetchProductRecords(IEnumerable<string> nhpids)
    {
        var productInfo = new ConcurrentDictionary<string, ProductBasicRecord>();

        await FetchMany<JsonArray>(
            nhpids.Select(id => $"{_lnhpdBaseUrl}/productlicence/?lang=en&id={id}"),
            r => r,
            rec => {
                productInfo[rec["lnhpd_id"]!.ToString()] = new ProductBasicRecord(
                    rec["lnhpd_id"]!.ToString(),
                    rec["licence_number"]!.GetValue<string>(), // What we're looking up
                    rec["product_name"]!.GetValue<string>(),
                    rec["company_name"]!.GetValue<string>(),
                    rec["flag_product_status"]!.GetValue<int>() == 1
                );
            }
        );

        return productInfo.ToImmutableDictionary();
    }

    private async Task<ImmutableDictionary<string, ImmutableList<string>>> FetchProductPurposes(IEnumerable<string> ids)
    {
        var productPurposes = new ConcurrentDictionary<string, ConcurrentBag<string>>();

        await FetchMany<JsonObject>(
            ids.Select(id => $"{_lnhpdBaseUrl}/productpurpose/?lang=en&id={id}"),
            r => r?["data"]?.AsArray(),
            rec =>
            {
                string key = rec["lnhpd_id"]!.ToString();
                string value = rec!["purpose"]!.GetValue<string>();
                ConcurrentBag<string> purposes = productPurposes.GetOrAdd(key, _ => new ConcurrentBag<string>());
                purposes.Add(value);
            }
        );

        return productPurposes.ToImmutableDictionary(e => e.Key, e => e.Value.ToImmutableList());
    }

    private async Task FetchMany<T>(IEnumerable<string> inputUrls, Func<T?, JsonArray?> finder, Action<JsonObject> processor) {
        using (var limiter = new Limiter(10))
        {
            await Task.WhenAll(inputUrls.Select(inputUrl => limiter.Wrap(async () =>
            {
                try {
                    var response = await _httpClient.GetFromJsonAsync<T>(inputUrl);
                    if (response != null) {
                        foreach (var record in finder(response) ?? new JsonArray())
                        {
                            if (record != null)
                            {
                                processor(record.AsObject());
                            }
                        }
                    }
                } catch (TaskCanceledException) {
                    return;
                }
            })));
        }
    }
}

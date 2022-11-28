namespace health_path.Model;

public record NaturalProduct(string LicenceNo, string ProductName, string CompanyName, bool Active, IEnumerable<string> Purposes);

using Microsoft.Extensions.Configuration;

namespace health_path;

class Program
{
    static void Main(string[] args) 
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddHttpClient();
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<System.Data.IDbConnection>(sp => new System.Data.SqlClient.SqlConnection(sp.GetRequiredService<IConfiguration>().GetConnectionString("Database")));

        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        //app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html");

        app.Run();
    }
}

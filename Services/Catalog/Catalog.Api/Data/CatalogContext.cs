using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public class CatalogContext
{
    public IMongoCollection<Product> Products { get; }
    private readonly ILogger<CatalogContext> _logger;

    public CatalogContext(IConfiguration configuration, ILogger<CatalogContext> logger)
    {
        _logger = logger;
        var mongoCatalogDbConnectionString = configuration.GetSection("DatabaseSettings_ConnectionString")?.Value
                                             ?? configuration.GetConnectionString("DefaultConnection");
        
        var mongoCatalogDbDatabaseName = configuration.GetConnectionString("DefaultConnection:DatabaseName")
            ?? "productDb";
        
        var collectionName = configuration.GetConnectionString("DefaultConnection:CollectionName")
            ?? "products";

        try
        {
            var client = new MongoClient(mongoCatalogDbConnectionString);
            var database = client.GetDatabase(mongoCatalogDbDatabaseName);
            Products = database.GetCollection<Product>(collectionName);
            
            CatalogContextSeed.SeedData(Products);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while initializing the CatalogContext.");
        }
    }
}
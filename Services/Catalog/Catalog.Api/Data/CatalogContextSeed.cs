using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Data;

public static class CatalogContextSeed
{
    internal static void SeedData(IMongoCollection<Product> productCollection)
    {
        var existProduct = productCollection.Find(p => true).Any();
        if (!existProduct)
        {
            productCollection.InsertManyAsync(GetPreconfiguredProducts());
        }
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new()
            {
                Id = "5f43f0f4b6b4a9f8a4e7a6b1",
                Name = "IPhone X",
                Summary =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new()
            {
                Id = "5f43f0f4b6b4a9f8a4e7a6b2",
                Name = "Samsung 10",
                Summary =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Category = "Smart Phone"
            },
            new()
            {
                Id = "5f43f0f4b6b4a9f8a4e7a6b3",
                Name = "Huawei Plus",
                Summary =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-3.png",
                Price = 650.00M,
                Category = "White Appliances"
            },
            new()
            {
                Id = "5f43f0f4b6b4a9f8a4e7a6b4",
                Name = "Xiaomi Mi 9",
                Summary =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                ImageFile = "product-4.png",
                Price = 470.00M,
                Category = "White Appliances"
            },
            new()
            {
                Id = "5f43f0f4b6b4a9f8a4e7a6b5",
                Name = "HTC U11+ Plus",
                Summary =
                    "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "This phone is the company's biggest change",
                ImageFile = "product-5.png",
                Price = 380.00M,
                Category = "Smart Phone"
            }
        };
    }
}
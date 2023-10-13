using Catalog.Api.Entities;

namespace Catalog.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetProductsAsync(int? take = null, int? skip = null);
    Task<Product?> GetProductAsync(string id);
    Task<IEnumerable<Product>?> GetProductsByNameAsync(string name);
    Task<IEnumerable<Product>?> GetProductsByCategoryAsync(string categoryName);
    Task CreateProductAsync(Product product);
    Task<bool> UpdateProductAsync(string id, Product? product, params string[]? updatedProperties);
    Task<bool> DeleteProductAsync(string id);
}
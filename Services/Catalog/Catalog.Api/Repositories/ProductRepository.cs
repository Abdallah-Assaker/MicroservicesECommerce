using System.Linq.Expressions;
using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;

namespace Catalog.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext _context;

    public ProductRepository(CatalogContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(int? take = null, int? skip = null)
    {
        var entities = _context.Products.Find(e => true);
        
        if (skip.HasValue)
        {
            entities = entities.Skip(skip.Value);
        }
        
        if (take.HasValue)
        {
            entities = entities.Limit(take.Value);
        }
        
        return await entities.ToListAsync().ConfigureAwait(false);
    }

    public async Task<Product?> GetProductAsync(string id)
    {
        var entity = await _context.Products.FindAsync(e => e.Id == id).ConfigureAwait(false);
        return await entity.FirstOrDefaultAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Product>?> GetProductsByNameAsync(string name)
    {
        var entities = await _context.Products.FindAsync(e => e.Name == name).ConfigureAwait(false);
        return await entities.ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Product>?> GetProductsByCategoryAsync(string categoryName)
    {
        var entities = await _context.Products.FindAsync(e => e.Category == categoryName).ConfigureAwait(false);
        return await entities.ToListAsync().ConfigureAwait(false);    }

    public async Task CreateProductAsync(Product product)
    {
        await _context.Products.InsertOneAsync(product).ConfigureAwait(false);
    }

    public async Task<bool> UpdateProductAsync(string id, Product? product, params string[]? updatedProperties)
    {
        if (product is null ||  updatedProperties is null || updatedProperties.Length == 0)
        {
            return false;
        }
        
        var update = Builders<Product>.Update;
        var updates = new List<UpdateDefinition<Product>>();
        
        foreach (var param in updatedProperties)
        {
            var entityProperty = typeof(Product).GetProperty(param);
            
            if (entityProperty is not null) updates.Add(update.Set(param, entityProperty.GetValue(product)));
        }

        if (updates.Count == 0) return false;
        
        var result = await _context.Products.UpdateOneAsync(e => e.Id == id, update.Combine(updates)).ConfigureAwait(false);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProductAsync(string id)
    {
        var result = await _context.Products.DeleteOneAsync(e => e.Id == id).ConfigureAwait(false);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }
    
    public async Task<int> CountAsync(Expression<Func<Product, bool>> criteria)
    {
        return (int) await _context.Products.CountDocumentsAsync(criteria).ConfigureAwait(false);
    }
}
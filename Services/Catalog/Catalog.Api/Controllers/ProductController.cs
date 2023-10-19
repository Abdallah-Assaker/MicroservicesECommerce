using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _repository;

    public ProductController(ILogger<ProductController> logger, IProductRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _repository.GetProductsAsync());
    }
    
    [HttpGet("{id:required:length(24)}")]
    public async Task<IActionResult> Get(string id)
    {
        var product = await _repository.GetProductAsync(id);
        if (product is null)
        {
            _logger.LogWarning("Product with id: {id} not found.", id);
            return NotFound();
        }

        return Ok(product);
    }
    
    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var products = await _repository.GetProductsByNameAsync(name);
        if (products is null)
        {
            _logger.LogWarning("Products with name: {name} not found.", name);
            return NotFound();
        }

        return Ok(products);
    }
    
    [HttpGet("category/{categoryName}")]
    public async Task<IActionResult> GetByCategory(string categoryName)
    {
        var products = await _repository.GetProductsByCategoryAsync(categoryName);
        if (products is null)
        {
            _logger.LogWarning("Products with category: {categoryName} not found.", categoryName);
            return NotFound();
        }

        return Ok(products);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Product product)
    {
        await _repository.CreateProductAsync(product);
        return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
    }
    
    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Put(string id, [FromBody] Product product)
    {
        var updated = await _repository.UpdateProductAsync(id, product, nameof(product.Price));
        if (!updated)
        {
            _logger.LogWarning("Product with id: {id} not found.", product);
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _repository.DeleteProductAsync(id);
        if (!deleted)
        {
            _logger.LogWarning("Product with id: {id} not found.", id);
            return NotFound();
        }

        return NoContent();
    }
}
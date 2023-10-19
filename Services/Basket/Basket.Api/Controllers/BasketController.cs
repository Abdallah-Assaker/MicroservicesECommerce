using Basket.Api.Entities;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> Get(string userName)
    {
        var basket = await _repository.GetBasket(userName);
        return Ok(basket);
    }
    
    [HttpPost("{userName}")]
    public async Task<IActionResult> Update(string userName, [FromBody] ShoppingCart basket)
    {
        var updatedBasket = await _repository.AddOrUpdateBasket(userName, basket);
        return Ok(updatedBasket);
    }
    
    [HttpDelete("{userName}")]
    public async Task<IActionResult> Delete(string userName)
    {
        await _repository.RemoveBasket(userName);
        return NoContent();
    }
}
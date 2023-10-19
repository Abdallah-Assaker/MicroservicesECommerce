using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }
    
    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName).ConfigureAwait(false);
        return string.IsNullOrEmpty(basket) ? new ShoppingCart(): JsonConvert.DeserializeObject<ShoppingCart>(basket)!;
    }

    public async Task<ShoppingCart> AddOrUpdateBasket(string userName, ShoppingCart basket)
    {
        var serializedBasket = JsonConvert.SerializeObject(basket);
        await _redisCache.SetStringAsync(userName, serializedBasket).ConfigureAwait(false);
        return await GetBasket(userName);
    }
    
    public async Task RemoveBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName).ConfigureAwait(false);
    }
}
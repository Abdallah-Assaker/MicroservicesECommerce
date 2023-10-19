using Basket.Api.Entities;

namespace Basket.Api.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> AddOrUpdateBasket(string userName, ShoppingCart basket);
    Task RemoveBasket(string userName);
}
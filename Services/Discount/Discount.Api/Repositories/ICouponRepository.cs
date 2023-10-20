using Discount.Api.Entities;

namespace Discount.Api.Repositories;

public interface ICouponRepository
{
    Task<Coupon?> GetCoupon(string productId);
    Task<Coupon?> CreateCoupon(Coupon coupon);
    Task<Coupon?> UpdateCoupon(Coupon coupon);
    Task<bool> DeleteCoupon(string productId);
}
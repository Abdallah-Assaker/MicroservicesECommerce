using Discount.Api.Entities;
using Discount.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CouponController : ControllerBase
{
    private readonly ICouponRepository _repository;

    public CouponController(ICouponRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet("{productId}")]
    public async Task<ActionResult<Coupon?>> GetCoupon(string productId)
    {
        var coupon = await _repository.GetCoupon(productId);
        return Ok(coupon);
    }
    
    [HttpPost]
    public async Task<ActionResult<Coupon?>> CreateCoupon([FromBody] Coupon coupon)
    {
        var createdCoupon = await _repository.CreateCoupon(coupon);
        return CreatedAtAction(nameof(GetCoupon), new { productId = createdCoupon?.ProductId }, createdCoupon);
    }
    
    [HttpPut]
    public async Task<ActionResult<Coupon?>> UpdateCoupon([FromBody] Coupon coupon)
    {
        var updatedCoupon = await _repository.UpdateCoupon(coupon);
        return Ok(updatedCoupon);
    }
    
    [HttpDelete("{productId}")]
    public async Task<ActionResult<bool>> DeleteCoupon(string productId)
    {
        var deleted = await _repository.DeleteCoupon(productId);
        return Ok(deleted);
    }
}
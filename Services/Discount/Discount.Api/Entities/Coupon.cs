namespace Discount.Api.Entities;

public record Coupon
{
    public int Id { get; init; }
    public string ProductId { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
}
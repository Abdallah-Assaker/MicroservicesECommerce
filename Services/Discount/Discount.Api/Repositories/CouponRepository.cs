using Dapper;
using Discount.Api.Entities;
using Npgsql;

namespace Discount.Api.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly string _connectionString;
    private NpgsqlConnection Connection => new (_connectionString);

    public CouponRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetSection("ConnectionStrings_DefaultConnection").Value
                           ?? configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<Coupon?> GetCoupon(string productId)
    {
        await using var connection = Connection;
        await connection.OpenAsync();

        var commandText = "SELECT * FROM Coupon WHERE ProductId = @ProductId ORDER BY Id DESC FETCH FIRST 1 ROWS ONLY";

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(commandText, new { ProductId = productId });

        return coupon;
    }
    
    public async Task<Coupon?> CreateCoupon(Coupon coupon)
    {
        await using var connection = Connection;
        await connection.OpenAsync();

        const string commandText = "INSERT INTO Coupon (ProductId, Description, Amount) VALUES (@ProductId, @Description, @Amount)";

        var affectedRows = await connection.ExecuteAsync(commandText, coupon);

        return affectedRows > 0 ? await GetCoupon(coupon.ProductId) : null;
    }

    public async Task<Coupon?> UpdateCoupon(Coupon coupon)
    {
        await using var connection = Connection;
        await connection.OpenAsync();
        
        const string commandText =
            $"""
             UPDATE Coupon 
             SET {nameof(Coupon.ProductId)} = @{nameof(Coupon.ProductId)}, 
             {nameof(Coupon.Description)} = @{nameof(Coupon.Description)}, 
             {nameof(Coupon.Amount)} = @{nameof(Coupon.Amount)} 
             WHERE {nameof(Coupon.Id)} = @{nameof(Coupon.Id)}
             """;
        
        var affectedRows = await connection.ExecuteAsync(commandText, coupon);
        
        return affectedRows > 0 ? await GetCoupon(coupon.ProductId) : null;
    }

    public async Task<bool> DeleteCoupon(string productId)
    {
        await using var connection = Connection;
        await connection.OpenAsync();
        
        const string commandText = $"DELETE FROM Coupon WHERE {nameof(Coupon.ProductId)} = @{nameof(Coupon.ProductId)}";

        var affectedRows = await connection.ExecuteAsync(commandText, productId);
        
        return affectedRows > 0;
    }
}
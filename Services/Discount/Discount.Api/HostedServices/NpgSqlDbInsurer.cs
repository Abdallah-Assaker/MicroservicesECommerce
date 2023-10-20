using Dapper;
using Npgsql;

namespace Discount.Api.HostedServices;

public class NpgSqlDbInsurer : IHostedService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<NpgSqlDbInsurer> _logger;

    public NpgSqlDbInsurer(IConfiguration configuration, ILogger<NpgSqlDbInsurer> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting NpgSqlDbInsurer");
        
        var connectionString = _configuration.GetSection("ConnectionStrings_DefaultConnection").Value;
        
        _logger.LogInformation("Connection string: {connectionString}", connectionString);
        
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        
        var textCommand = """
                          CREATE TABLE IF NOT EXISTS Coupon(Id SERIAL PRIMARY KEY,
                                                          ProductName VARCHAR(24) NOT NULL,
                                                          Description TEXT,
                                                          Amount INT)
                          """;

        await connection.ExecuteAsync(textCommand);
        
        _logger.LogInformation("Finished NpgSqlDbInsurer");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
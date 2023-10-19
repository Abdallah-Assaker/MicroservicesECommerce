using Basket.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(opt => 
    {
    opt.Configuration = builder.Configuration.GetSection("CacheSettings_ConnectionString").Value
        ?? builder.Configuration.GetConnectionString("RedisConnection") ;
    opt.InstanceName = "Basket";
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

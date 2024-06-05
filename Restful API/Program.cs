using Microsoft.Extensions.Caching.Memory;
using NLog.Extensions.Logging;
using Restful_API.Repositories;
using Restful_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(logger =>
{
    logger.ClearProviders();
    logger.SetMinimumLevel(LogLevel.Debug);
});

builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<OrderRepo>();
builder.Services.AddScoped<IOrderRepo>(provider =>
{
    var orderRepo = provider.GetRequiredService<OrderRepo>();
    var memoryCache = provider.GetRequiredService<IMemoryCache>();

    return new CacheOrderRepo(orderRepo, memoryCache);
});
builder.Services.AddScoped<OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

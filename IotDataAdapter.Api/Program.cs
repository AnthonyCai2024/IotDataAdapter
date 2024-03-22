using System.Net.Sockets;
using IotDataAdapter.Api.Services;
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;
using IotDataAdapter.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//custom register
builder.Services.AddControllers();
// 注册连接策略
builder.Services.AddTransient<IConnectionStrategy<TcpParameter, UdpClient, byte[]>, UdpConnectionStrategy>();
builder.Services.AddTransient<IDataCollectionService, DataCollectionService>();
builder.Services.AddSingleton<IRedisConnection>(
    new RedisConnection("36.137.225.245:6376,password=mtic0756-dev,DefaultDatabase=5"));
// builder.Services.AddTransient<IRedisService, RedisService>();
// builder.Services.AddTransient<IRedisService, RedisService>();
builder.Services.AddTransient<IRedisService>(provider =>
{
    var connection = provider.GetRequiredService<IRedisConnection>();
    const int database = 5; // 你可以从配置文件或环境变量中获取这个值
    return new RedisService(connection, database);
});

// // 注册数据协议策略
// builder.Services.AddTransient<IDataProtocolStrategy, ModbusProtocolStrategy>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
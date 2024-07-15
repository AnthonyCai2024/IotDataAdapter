using Castle.DynamicProxy;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// 注册服务和拦截器
builder.Services.AddTransient<IMyService, MyService>();

// 注册 Castle 动态代理生成器
builder.Services.AddSingleton<IProxyGenerator, ProxyGenerator>();

// 注册自定义的拦截器
builder.Services.AddSingleton<CustomInterceptor>();

// 使用工厂模式创建代理
builder.Services.AddTransient<IMyService>(provider =>
{
    var proxyGenerator = provider.GetRequiredService<IProxyGenerator>();
    var interceptor = provider.GetRequiredService<CustomInterceptor>();
    var myService = new MyService();
    return proxyGenerator.CreateInterfaceProxyWithTarget<IMyService>(myService, interceptor);
});

// 注册 BackgroundService
builder.Services.AddHostedService<MyBackgroundService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// YamlService.LoadYaml();

// new CryptHelper().Test();

// ObserverMain.Test(null);


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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
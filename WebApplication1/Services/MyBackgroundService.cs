namespace WebApplication1.Services;

public class MyBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // 模拟后台任务
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var myService = scope.ServiceProvider.GetRequiredService<IMyService>();

                myService.MyMethod(); // 这个方法会被拦截
                myService.AnotherMethod(); // 这个方法不会被拦截
            }

            await Task.Delay(1000, stoppingToken); // 每秒执行一次
        }
    }
}
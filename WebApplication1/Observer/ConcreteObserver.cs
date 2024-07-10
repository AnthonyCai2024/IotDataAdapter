namespace WebApplication1.Observer;

public class ConcreteObserver : IObserver
{
    private string _cacheData;

    public void Update(string data)
    {
        // 模拟缓存更新逻辑  
        _cacheData = data;
        Console.WriteLine($"Observer: Cache updated with new data - {_cacheData}");
    }
}
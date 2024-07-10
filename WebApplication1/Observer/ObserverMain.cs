namespace WebApplication1.Observer;

public static class ObserverMain
{
    public static void Test(string[] args)
    {
        var subject = new ConcreteSubject();
        var observer1 = new ConcreteObserver();
        var observer2 = new ConcreteObserver();

        subject.RegisterObserver(observer1);
        subject.RegisterObserver(observer2);

        subject.Data = "New data"; // 触发通知，观察者更新缓存  

        // 取消注册一个观察者  
        subject.UnregisterObserver(observer1);

        subject.Data = "Another new data"; // 仅observer2会收到通知并更新缓存  
    }
}
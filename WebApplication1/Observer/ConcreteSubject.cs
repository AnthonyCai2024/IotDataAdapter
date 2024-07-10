namespace WebApplication1.Observer;

public class ConcreteSubject : ISubject
{
    private readonly List<IObserver> _observers = [];
    private string _data;

    public string Data
    {
        get => _data;
        set
        {
            _data = value;
            NotifyObservers();
        }
    }

    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(_data);
        }
    }
}
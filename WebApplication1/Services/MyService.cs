namespace WebApplication1.Services;

public class MyService : IMyService
{
    [Intercept]
    public void MyMethod()
    {
        Console.WriteLine("Executing MyMethod");
    }

    public void AnotherMethod()
    {
        Console.WriteLine("Executing AnotherMethod");
    }
}
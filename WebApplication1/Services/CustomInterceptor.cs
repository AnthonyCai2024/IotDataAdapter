using Castle.DynamicProxy;

namespace WebApplication1.Services;

public class CustomInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        var method = invocation.Method;
        var shouldIntercept = method.IsDefined(typeof(InterceptAttribute), true);

        if (shouldIntercept)
        {
            try
            {
                // 方法前
                Console.WriteLine($"Before executing {method.Name}");

                invocation.Proceed();

                // 方法后
                Console.WriteLine($"After executing {method.Name}");
            }
            catch (Exception ex)
            {
                // 异常时
                Console.WriteLine($"Exception in {method.Name}: {ex.Message}");
                throw;
            }
        }
        else
        {
            invocation.Proceed();
        }
    }
}
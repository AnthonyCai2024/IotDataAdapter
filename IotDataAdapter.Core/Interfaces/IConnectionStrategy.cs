namespace IotDataAdapter.Core.Interfaces;

public interface IConnectionStrategy<in TRequest, TClient, TResponse>
{
    Task<TClient> ConnectAsync(TRequest request);

    Task<TResponse> SendAsync(TClient udpClient, TRequest request);
    // Task<string> ReceiveAsync(T connection);
}
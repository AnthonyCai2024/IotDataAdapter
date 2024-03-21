using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class UdpConnectionStrategy : IConnectionStrategy<TcpParameter>
{
    public Task ConnectAsync(TcpParameter parameter)
    {
        throw new NotImplementedException();
    }
}
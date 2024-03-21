using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class Rs485ConnectionStrategy : IConnectionStrategy<TcpParameters>
{
    public Task ConnectAsync(TcpParameters parameters)
    {
        throw new NotImplementedException();
    }
}
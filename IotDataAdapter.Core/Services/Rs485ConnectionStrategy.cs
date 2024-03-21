using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class Rs485ConnectionStrategy : IConnectionStrategy<TcpParameter>
{
    public Task ConnectAsync(TcpParameter parameter)
    {
        throw new NotImplementedException();
    }
}
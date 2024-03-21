using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class OpcProtocolStrategy : IProtocolStrategy<OpcParameter, ushort[]>
{
    public Task<ushort[]> ProcessDataAsync(OpcParameter parameter)
    {
        throw new NotImplementedException();
    }
}
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class OpcProtocolStrategy : IProtocolStrategy<OpcParameters, ushort[]>
{
    public Task<ushort[]> ProcessDataAsync(OpcParameters parameters)
    {
        throw new NotImplementedException();
    }
}
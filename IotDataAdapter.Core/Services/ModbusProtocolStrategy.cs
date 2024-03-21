using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class ModbusProtocolStrategy: IProtocolStrategy<ModbusParameters, ushort[]>
{
    public Task<ushort[]> ProcessDataAsync(ModbusParameters parameters)
    {
        throw new NotImplementedException();
    }
}

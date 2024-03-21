using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class ModbusProtocolStrategy: IProtocolStrategy<ModbusParameter, ushort[]>
{
    public Task<ushort[]> ProcessDataAsync(ModbusParameter parameter)
    {
        throw new NotImplementedException();
    }
}

using Mtim.Grpc.Modbus.Models;

namespace Mtim.Grpc.Modbus.Services;

public class ModbusTcpService : IModbusService
{
    public Task<ushort[]?> ModbusTcpMasterReadHoldingRegisters()
    {
        throw new NotImplementedException();
    }

    public Task<ushort[]?> ReadHoldingRegisters(ModbusRequest request)
    {
        throw new NotImplementedException();
    }
}
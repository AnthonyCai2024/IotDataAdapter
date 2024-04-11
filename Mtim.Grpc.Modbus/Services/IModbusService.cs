using Mtim.Grpc.Modbus.Models;

namespace Mtim.Grpc.Modbus.Services;

public interface IModbusService
{
    Task<ushort[]?> ReadHoldingRegisters(ModbusRequest request);
}
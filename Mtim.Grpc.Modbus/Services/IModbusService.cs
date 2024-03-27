using Mtim.Grpc.Modbus.Models;

namespace Mtim.Grpc.Modbus.Services;

public interface IModbusService
{
    Task<ushort[]?> ModbusTcpMasterReadHoldingRegisters();

    Task<ushort[]?> ModbusUdpMasterReadRegisters(ModbusRequest request);
}
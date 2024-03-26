using IotDataAdapter.Modbus.Models;

namespace IotDataAdapter.Modbus.Services;

public interface IModbusService
{
    Task<ushort[]?> ModbusTcpMasterReadHoldingRegisters();

    Task<ushort[]?> ModbusUdpMasterReadRegisters(ModbusRequest request);
}
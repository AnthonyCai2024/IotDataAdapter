namespace IotDataAdapter.Modbus.Services;

public interface IModbusService
{
    Task<ushort[]?> ModbusTcpMasterReadHoldingRegisters();

    Task<ushort[]> ModbusUdpMasterReadRegisters();
}
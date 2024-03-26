namespace IotDataAdapter.Modbus.Models;

public class ModbusRequest
{
    public required string Ip { get; init; }
    public required ushort Port { get; init; }
    public required byte SlaveId { get; init; }
    public required ushort StartAddress { get; init; }
    public required ushort NumInputs { get; init; }
    public ushort Val;
}
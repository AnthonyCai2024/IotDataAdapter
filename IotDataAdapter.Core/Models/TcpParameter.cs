namespace IotDataAdapter.Core.Models;

public class TcpParameter
{
    public required string Ip;
    public ushort Slave { get; init; } = 1;
    public ushort Start { get; init; } = 1;
    public ushort Length { get; init; } = 1;
    public required ushort Port { get; init; }

    /// <summary>
    ///  指定PLC的基地址, 只有0和1两种选择, default 1
    /// </summary>
    public required ushort PlcBaseAddress { get; init; } = 1;

    /// <summary>
    /// write value
    /// </summary>
    public ushort Val { get; init; } = 0;
}
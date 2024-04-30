namespace Mtim.ProtoHub.WebApp.Protocol;

public record ModbusTcpParameters : ICommandParameters
{
    public required string Ip { get; set; }
    public required byte Slave { get; set; }
    public required ushort Start { get; set; }
    public required ushort Val { get; set; }

    /// <summary>
    ///  指定PLC的基地址, 只有0和1两种选择, default 1
    /// </summary>
    public required ushort PlcBaseAddress { get; init; } = 1;
}
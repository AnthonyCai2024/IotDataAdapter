namespace Mtim.ProtoHub.WebApp.Protocol;

public class ModbusTcpParameters : ICommandParameters
{
    public required string Ip { get; set; }
    public required ushort Slave { get; set; }
    public required ushort Start { get; set; }
    public required ushort Val { get; set; }
}
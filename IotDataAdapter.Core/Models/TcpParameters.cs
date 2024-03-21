namespace IotDataAdapter.Core.Models;

public class TcpParameters
{
    public required string Ip;
    public ushort Slave = 1;
    public ushort Start = 1;
    public ushort Length = 1;
}
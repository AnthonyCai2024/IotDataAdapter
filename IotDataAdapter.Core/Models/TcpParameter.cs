namespace IotDataAdapter.Core.Models;

public class TcpParameter
{
    public required string Ip;
    public ushort Slave = 1;
    public ushort Start = 1;
    public ushort Length = 1;
    public ushort Port = 502;
}
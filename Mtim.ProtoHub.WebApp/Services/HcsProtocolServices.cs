using System.Net;
using System.Net.Sockets;
using IotDataAdapter.Core.Enum;
using IotDataAdapter.Core.Utils;
using Mtim.ProtoHub.WebApp.Protocol;

namespace Mtim.ProtoHub.WebApp.Services;

public class HcsProtocolServices : HexCommandBuilder, IProtocolStrategy
{
    private async Task WriteSingle(string ip, byte slave, ushort? start, ushort length = 1)
    {
        using var udpClient = new UdpClient();
        udpClient.Client.ReceiveTimeout = 100;
        udpClient.Connect(ip, 1086);

        var sendBytes = HexBuild(slave, start - 1, length, 0, command: ModbusCommand.WriteSingle);

        await udpClient.SendAsync(sendBytes, sendBytes.Length);

        var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        // var result = udpClient.Receive(ref remoteEndPoint);
        udpClient.Receive(ref remoteEndPoint);
    }

   


    public async Task WriteSingle(ICommandParameters parameters)
    {
        if (parameters is ModbusTcpParameters tcpParameter)
        {
            await WriteSingle(tcpParameter.Ip, tcpParameter.Slave, tcpParameter.Start, tcpParameter.Val);
        }
        else
        {
            throw new ArgumentException("Invalid parameters");
        }
    }
}
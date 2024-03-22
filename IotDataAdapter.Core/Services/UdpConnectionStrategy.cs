using System.Net;
using System.Net.Sockets;
using IotDataAdapter.Core.Enum;
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;
using IotDataAdapter.Core.Utils;

namespace IotDataAdapter.Core.Services;

public class UdpConnectionStrategy : HexCommandBuilder, IConnectionStrategy<TcpParameter, UdpClient, byte[]>
{
    public Task<UdpClient> ConnectAsync(TcpParameter request)
    {
        // no need to connect
        return Task.FromResult(new UdpClient());
    }

    public async Task<byte[]> SendAsync(UdpClient udpClient, TcpParameter request)
    {
        // set timeout
        udpClient.Client.ReceiveTimeout = 100;
        // udp no need to connect, just send
        // udpClient.Connect(ip, 1086);

        var sendBytes = HexBuild(request.Slave
            , request.Start - request.PlcBaseAddress
            , request.Length
            , request.Val
            , ModbusCommand.Read);

        await udpClient.SendAsync(sendBytes, sendBytes.Length);

        var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

        var result = udpClient.Receive(ref remoteEndPoint);

        // 打印接收到的数据
        ShowByteData(result);

        return result;
    }
}
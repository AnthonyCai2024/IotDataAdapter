using System.Net;
using System.Net.Sockets;
using System.Text;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class DevicePollingService(List<TcpParameter> devices, TimeSpan pollingInterval, TimeSpan operationTimeout)
{
    public async Task PollDevicesAsync()
    {
        while (true) // 持续轮询
        {
            foreach (var device in devices)
            {
                try
                {
                    using var udpClient = new UdpClient();
                    udpClient.Client.ReceiveTimeout = 100;
                    udpClient.Connect(device.Ip, 1086);
                    // await tcpClient.ConnectAsync(device.Ip, device.Port);
                    Console.WriteLine($"Connected to {device.Ip}:{device.Port}, ready to read data.");
                    // 实现数据读取逻辑
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to connect or read from {device.Ip}:{device.Port}. Error: {ex.Message}");
                    // 处理连接失败或读取数据异常
                }
            }

            Console.WriteLine($"Waiting for {pollingInterval.TotalSeconds} seconds before next poll.");
            await Task.Delay(pollingInterval); // 等待一分钟再次轮询
        }
    }

    private async Task PollDeviceAsync(TcpParameter device, CancellationToken cancellationToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(operationTimeout); // 设置操作超时


        using var udpClient = new UdpClient();

        var endpoint = new IPEndPoint(IPAddress.Parse(device.Ip), device.Port);

        // 准备数据包
        var message = Encoding.UTF8.GetBytes("Your message here");
        await udpClient.SendAsync(message, message.Length, endpoint);
        Console.WriteLine($"Sent request to {device.Ip}.");

        // 接收响应
        // var response = await udpClient.ReceiveAsync(cancellationToken);
        // var responseMessage = Encoding.UTF8.GetString(response.Buffer);
        // 尝试接收数据，等待可能因超时而取消
        // var response = await udpClient.ReceiveAsync(cts.Token);
        //
        // // 处理响应
        // Console.WriteLine($"Received response from {device.Ip}: {response}");
    }

    public async Task StartPollingAsync()
    {
        var cts = new CancellationTokenSource();
        //
        // while (!cts.Token.IsCancellationRequested)
        // {
        var tasks = new List<Task>();
        foreach (var device in devices)
        {
            tasks.Add(PollDeviceAsync(device, cts.Token));
        }

        await Task.WhenAll(tasks);

        Console.WriteLine($"Waiting for {pollingInterval.TotalSeconds} seconds before next poll.");
        // await Task.Delay(pollingInterval, cts.Token);
        // }
    }
}
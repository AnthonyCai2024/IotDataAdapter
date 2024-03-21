﻿using System.Net.Sockets;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Core.Services;

public class DevicePollingService(List<TcpParameter> devices, TimeSpan pollingInterval)
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
}
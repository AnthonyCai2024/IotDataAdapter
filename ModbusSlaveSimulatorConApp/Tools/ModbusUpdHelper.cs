using System.Net;
using System.Net.Sockets;
using NModbus;

namespace ModbusSlaveSimulatorConApp.Tools;

public static class ModbusUpdHelper
{
    public static Task ReadData()
    {
        using var client = new UdpClient();
        var endPoint = new IPEndPoint(IPAddress.Parse("192.168.4.33"), 1086);
        client.Connect(endPoint);

        var factory = new ModbusFactory();

        var master = factory.CreateMaster(client);

        const ushort startAddress = 0;

        // // write three coils
        // master.WriteMultipleCoils(0, startAddress, new bool[] { true, false, true });

        var result = master.ReadHoldingRegisters(1, startAddress, 50);
        Console.WriteLine("Registers: " + string.Join(", ", result));

        return Task.CompletedTask;
    }
}
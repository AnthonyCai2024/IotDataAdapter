using System.Net;
using System.Net.Sockets;
using Mtim.Grpc.Modbus.Models;
using NModbus;

namespace Mtim.Grpc.Modbus.Services;

public class ModbusUdpService : IModbusService
{
    public async Task<ushort[]?> ReadHoldingRegisters(ModbusRequest request)
    {
        using var client = new UdpClient();
        var endPoint = new IPEndPoint(IPAddress.Parse(request.Ip), 1086);
        client.Client.ReceiveTimeout = 200;
        client.Connect(endPoint);

        var factory = new ModbusFactory();

        var master = factory.CreateMaster(client);

        var registers =
            await master.ReadHoldingRegistersAsync(request.SlaveId,
                (ushort)(request.StartAddress - request.PlcBaseAddress),
                request.NumInputs);

        Console.WriteLine("Registers: " + string.Join(", ", registers));

        return registers;
    }
}
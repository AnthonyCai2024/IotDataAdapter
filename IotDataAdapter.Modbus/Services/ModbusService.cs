using System.Net;
using System.Net.Sockets;
using IotDataAdapter.Modbus.Models;
using NModbus;

namespace IotDataAdapter.Modbus.Services;

public class ModbusService : IModbusService
{
    /// <summary>
    ///     Simple Modbus TCP master read inputs example.
    /// </summary>
    public async Task<ushort[]?> ModbusTcpMasterReadHoldingRegisters()
    {
        using var client = new TcpClient("192.168.4.43", 502);
        var factory = new ModbusFactory();
        var master = factory.CreateMaster(client);


        byte slaveId = 1;
        ushort startAddress = 1;
        ushort numInputs = 5;

        // UInt32 www = 0x42c80083;
        //
        // master.WriteSingleRegister32(slaveId, startAddress, www);
        var registers = await master.ReadHoldingRegistersAsync(slaveId, startAddress, numInputs);

        for (var i = 0; i < numInputs; i++)
        {
            Console.WriteLine($"Input {(startAddress + i)}={registers[i]}");
        }

        return registers;
    }

    public async Task<ushort[]?> ModbusUdpMasterReadRegisters(ModbusRequest request)
    {
        using var client = new UdpClient();
        var endPoint = new IPEndPoint(IPAddress.Parse(request.Ip), 1086);
        client.Client.ReceiveTimeout = 150;
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
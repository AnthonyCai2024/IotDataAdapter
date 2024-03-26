using System.Net;
using System.Net.Sockets;
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

    public async Task<ushort[]> ModbusUdpMasterReadRegisters()
    {
        using var client = new UdpClient();
        var endPoint = new IPEndPoint(IPAddress.Parse("192.168.4.32"), 1086);
        client.Client.ReceiveTimeout = 150;
        client.Connect(endPoint);

        var factory = new ModbusFactory();

        var master = factory.CreateMaster(client);

        ushort startAddress = 1;

        ushort numInputs = 5;

        // write three coils
        var registers = await master.ReadHoldingRegistersAsync(0, startAddress, numInputs);

        return registers;
    }
}
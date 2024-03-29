// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using NModbus;

Console.WriteLine("Hello, World!");

int port = 502;
IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

// create and start the TCP slave
TcpListener slaveTcpListener = new TcpListener(address, port);
slaveTcpListener.Start();

IModbusFactory factory = new ModbusFactory();

IModbusSlaveNetwork network = factory.CreateSlaveNetwork(slaveTcpListener);

IModbusSlave slave1 = factory.CreateSlave(1);
IModbusSlave slave2 = factory.CreateSlave(2);

network.AddSlave(slave1);
network.AddSlave(slave2);

// 生成随机值
var random = new Random();
var values = new List<float>();
for (int i = 0; i < 10000; i++)
{
    var value = (float)random.NextDouble();
    values.Add(value);
}

// 分配随机值到地址
ushort startAddress = 0;
var writeData = new ushort[values.Count];
for (int i = 0; i < values.Count; i++)
{
    writeData[i] = BitConverter.ToUInt16(BitConverter.GetBytes(values[i]), 0);
}

var writeDataStore = slave1.DataStore;
// writeDataStore.SyncRoot.EnterWriteLock();
try
{
    writeDataStore.HoldingRegisters.WritePoints(startAddress, writeData);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

network.ListenAsync().GetAwaiter().GetResult();

// prevent the main thread from exiting
Thread.Sleep(Timeout.Infinite);
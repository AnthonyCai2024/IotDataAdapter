// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Sockets;
using ModbusSlaveSimulatorConApp.Tools;
using NModbus;

Console.WriteLine("Hello, modbus slave simulator!");

const int port = 502;
const ushort startAddress = 0;
// IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

// create and start the TCP slave
var slaveTcpListener = new TcpListener(IPAddress.Any, port);
slaveTcpListener.Start();

var factory = new ModbusFactory();

IModbusSlaveNetwork network = factory.CreateSlaveNetwork(slaveTcpListener);

// var slave1 = factory.CreateSlave(1);
// var slave2 = factory.CreateSlave(2);

var slaveList = SlaveHelper.CreateSlaves(factory, 2);

foreach (var slave in slaveList)
{
    // add slaves to network
    network.AddSlave(slave);

    // create data store
    var writeDataStore = slave.DataStore;
    // writeDataStore.SyncRoot.EnterWriteLock();
    try
    {
        var writeData = DataStoreHelper.CreateRandomData();
        writeDataStore.HoldingRegisters.WritePoints(startAddress, writeData);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}


// 间隔1秒钟，写入寄存器
var task = Task.Run(async () =>
{
    while (true)
    {
        // 在这里编写需要每秒执行的代码
        Console.WriteLine("Code executed at: " + DateTime.Now);

        // 等待1秒
        await Task.Delay(1000);

        // write
        using var client = new TcpClient("127.0.0.1", 502);
        var master = factory.CreateMaster(client);


        const byte slaveId = 1;
        const ushort numInputs = 100;

        var writeData = DataStoreHelper.CreateRandomData(numInputs);

        await master.WriteMultipleRegistersAsync(slaveId, startAddress, writeData);
    }
});

network.ListenAsync().GetAwaiter().GetResult();

Console.WriteLine("modbus slave simulator now running!");


// 等待用户按下任意键来停止执行
Console.ReadKey();

// 取消任务并等待完成
var cts = new CancellationTokenSource();
cts.Cancel();
await task;

Console.WriteLine("Code execution stopped.");


// prevent the main thread from exiting
Thread.Sleep(Timeout.Infinite);
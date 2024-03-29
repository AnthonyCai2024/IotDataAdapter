// See https://aka.ms/new-console-template for more information


using System.Net.Sockets;
using ModbusSlaveSimulatorConApp.Tools;
using NModbus;

var serverTask =Task.Run(() =>
{
    ModbusSlaveSimulator.Run();
    return Task.CompletedTask;
});

// 等待一段时间，以确保Modbus Slave服务器已经启动
await Task.Delay(1000);

// 启动Modbus客户端
// 间隔1秒钟，写入寄存器
var clientTask = Task.Run(async () =>
{
    var factory = new ModbusFactory();

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

        await master.WriteMultipleRegistersAsync(slaveId, 1, writeData);
    }
});

// 等待用户按下任意键来停止执行
Console.WriteLine("Press any key to stop.");

Console.ReadKey();

// 停止Modbus Slave服务器
// TODO: 停止Modbus Slave服务器的逻辑

// 等待任务完成
await Task.WhenAll(serverTask, clientTask);

Console.WriteLine("Execution stopped.");


Console.WriteLine("modbus slave simulator now running!");


//
// network.ListenAsync().GetAwaiter().GetResult();
//
// Console.WriteLine("modbus slave simulator now running!");
//
//
// // 等待用户按下任意键来停止执行
// Console.ReadKey();
//
// // 取消任务并等待完成
// var cts = new CancellationTokenSource();
// cts.Cancel();
// await task;
//
// Console.WriteLine("Code execution stopped.");
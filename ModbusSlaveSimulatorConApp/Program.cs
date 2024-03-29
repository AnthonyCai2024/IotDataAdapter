// See https://aka.ms/new-console-template for more information


using System.Net.Sockets;
using ModbusSlaveSimulatorConApp.Tools;
using NModbus;

const byte count = 3;

Console.WriteLine("Code execution started. Press any key to stop.");

var taskList = new List<Task> { Task.Run(() => ModbusSlaveSimulator.Run(count)) };

// 启动代码执行任务
var taskClient = Task.Run(async () =>
{
    await Task.Delay(1000);

    using var client = new TcpClient("127.0.0.1", 502);
    var factory = new ModbusFactory();
    var master = factory.CreateMaster(client);

    while (true)
    {
        // 调用第二个方法，例如向Modbus写入随机值
        ModbusClientHelper.WriteTasks(master, count);

        // 等待1秒
        await Task.Delay(500);
        Console.WriteLine("Writing to slaves...");
    }
});

taskList.Add(taskClient);

await Task.WhenAll(taskList);

Console.WriteLine("Code execution stopped.");
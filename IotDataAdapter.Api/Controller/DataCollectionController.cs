using Grpc.Net.Client;
using IotDataAdapter.Api.Services;
using IotDataAdapter.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Mtim.Grpc.Modbus;

namespace IotDataAdapter.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DataCollectionController(IDataCollectionService dataCollectionService)
{
    [HttpPost("collectSingleData")]
    public void CollectSingleData()
    {
        var para = new TcpParameter
        {
            Slave = 1,
            Start = 1,
            Length = 1,
            Val = 1,
            PlcBaseAddress = 1,
            Ip = "192.168.4.41",
            Port = 1086
        };

        dataCollectionService.CollectSingleDataAsync(para);
    }

    [HttpPost("CollectMultiDataAsync")]
    public async Task CollectMultiDataAsync(string protocol = "udp")
    {
        // await dataCollectionService.CollectMultiDataAsync(GetParas());

        // The port number must match the port of the gRPC server.
        // using var channel = GrpcChannel.ForAddress("http://localhost:15248");
        // // var client = new Greeter.GreeterClient(channel);
        // // var reply = await client.SayHelloAsync(
        // //     new HelloRequest { Name = "GreeterClient" });
        //
        // var client = new ModbusService.ModbusServiceClient(channel);
        // var reply = client.ModbusGrpcUdpMasterReadRegisters(new ModbusUdpRequest
        // {
        //     Ip = "192.168.4.32",
        //     Port = 1086,
        //     SlaveId = 1,
        //     StartAddress = 1,
        //     NumInputs = 10,
        //     // no need val
        //     Val = 0
        // });
        // Console.WriteLine("Greeting: " + reply.Items.Count);
        // Console.WriteLine("Press any key to exit...");
        // Console.ReadKey();

        // The port number must match the port of the gRPC server.
        using var channel = GrpcChannel.ForAddress("http://localhost:5146");
        var client = new Greeter.GreeterClient(channel);

        var reply = await client.SayHelloAsync(
            new HelloRequest { Name = "GreeterClient" });

        Console.WriteLine("Greeting: " + reply.Message);

        var modbusClient = new ModbusService.ModbusServiceClient(channel);
        var modbusReply = modbusClient.ModbusGrpcUdpMasterReadRegisters(new ModbusUdpRequest
        {
            Ip = "192.168.4.32",
            Port = 1086,
            SlaveId = 1,
            StartAddress = 1,
            NumInputs = 10,
            Protocol = protocol
            // no need val
        });

        Console.WriteLine("modbus list: " + string.Join(", ", modbusReply.Items));
    }

    [HttpPost("ParallelCollectMultiDataAsync")]
    public async Task ParallelCollectMultiDataAsync()
    {
        await dataCollectionService.ParallelCollectMultiDataAsync(GetParas());
    }


    [HttpGet("{command}")]
    public string ExecuteCommand(string command)
    {
        return command;
    }

    private static List<TcpParameter> GetParas()
    {
        var ipList = Enumerable.Range(31, 10);

        const int slave = 1;
        const int start = 1;
        const int length = 1;
        const int val = 1;
        const int plcBaseAddress = 1;
        const string ip = "192.168.4.";
        const int port = 1086;

        return ipList.Select(ipItem => new TcpParameter
            {
                Slave = slave,
                Start = start,
                Length = length,
                Val = val,
                PlcBaseAddress = plcBaseAddress,
                Ip = ip + ipItem,
                Port = port
            })
            .ToList();
    }
}
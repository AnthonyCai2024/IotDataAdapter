using IotDataAdapter.Api.Services;
using IotDataAdapter.Core.Models;
using Microsoft.AspNetCore.Mvc;

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
    public async Task CollectMultiDataAsync()
    {
        await dataCollectionService.CollectMultiDataAsync(GetParas());
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
        var ipList = Enumerable.Range(31, 44);

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
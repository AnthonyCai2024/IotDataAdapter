using IotDataAdapter.Api.Services;
using IotDataAdapter.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace IotDataAdapter.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class DataCollectionController(IDataCollectionService dataCollectionService)
{
  

    [HttpPost("collectData")]
    public void GetUdpData()
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

        dataCollectionService.CollectDataAsync(para);
    }

   

    [HttpGet("{command}")]
    public string ExecuteCommand(string command)
    {
        return command;
    }
}
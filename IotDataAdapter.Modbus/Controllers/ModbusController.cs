using IotDataAdapter.Modbus.Services;
using Microsoft.AspNetCore.Mvc;

namespace IotDataAdapter.Modbus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModbusController(IModbusService modbusService)
{
    [HttpPost("CollectMultiDataAsync")]
    public async Task CollectMultiDataAsync()
    {
        await modbusService.ModbusUdpMasterReadRegisters();
    }

    [HttpPost("ParallelCollectMultiDataAsync")]
    public async Task ParallelCollectMultiDataAsync()
    {
        Console.WriteLine("ParallelCollectMultiDataAsync");
    }
}
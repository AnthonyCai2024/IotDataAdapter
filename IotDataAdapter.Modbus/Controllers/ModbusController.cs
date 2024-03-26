using IotDataAdapter.Modbus.Models;
using IotDataAdapter.Modbus.Services;
using Microsoft.AspNetCore.Mvc;

namespace IotDataAdapter.Modbus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModbusController(IModbusService modbusService)
{
    [HttpPost("ModbusUdpMasterReadRegisters")]
    public async Task<ushort[]?> ModbusUdpMasterReadRegisters(ModbusRequest request)
    {
        var resp = await modbusService.ModbusUdpMasterReadRegisters(request);

        return resp;
    }

    [HttpPost("ParallelCollectMultiDataAsync")]
    public async Task ParallelCollectMultiDataAsync()
    {
        Console.WriteLine("ParallelCollectMultiDataAsync");
    }
}
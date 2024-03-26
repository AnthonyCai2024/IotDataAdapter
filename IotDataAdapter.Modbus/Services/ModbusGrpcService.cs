using Grpc.Core;
using Iot.modbus;
using IotDataAdapter.Modbus.Models;

namespace IotDataAdapter.Modbus.Services;

public class ModbusGrpcService(IModbusService modbusService) : Iot.modbus.ModbusService.ModbusServiceBase
{
    public override async Task<UShortArray> ModbusGrpcUdpMasterReadRegisters(ModbusUdpRequest request,
        ServerCallContext context)
    {
        var resp = await modbusService.ModbusUdpMasterReadRegisters(new ModbusRequest
        {
            Ip = request.Ip,
            Port = (ushort)request.Port,
            SlaveId = (byte)request.SlaveId,
            StartAddress = (ushort)request.StartAddress,
            NumInputs = (ushort)request.NumInputs
        });

        UShortArray ushortArray = new();
        if (resp is not { Length: > 0 }) return ushortArray;
        foreach (var r in resp)
        {
            ushortArray.Items.Add(r);
        }

        return ushortArray;
    }
}
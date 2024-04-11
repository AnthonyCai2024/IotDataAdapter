using Grpc.Core;
using Mtim.Grpc.Modbus.Models;

namespace Mtim.Grpc.Modbus.Services;

public class ModbusGrpcService(IModbusServiceFactory serviceFactory) : ModbusService.ModbusServiceBase
{
    public override async Task<UShortArray> ModbusGrpcUdpMasterReadRegisters(ModbusUdpRequest request,
        ServerCallContext context)
    {
        var protocol = request.Protocol.Equals("tcp", StringComparison.CurrentCultureIgnoreCase)
            ? "tcp"
            : "udp";
        
        // Create a new instance of the ModbusServiceFactory class
        var modbusService = serviceFactory.CreateModbusService(protocol);
        
        var resp = await modbusService.ReadHoldingRegisters(new ModbusRequest
        {
            Ip = request.Ip,
            Port = (ushort)request.Port,
            SlaveId = (byte)request.SlaveId,
            StartAddress = (ushort)request.StartAddress,
            NumInputs = (ushort)request.NumInputs,
            // PlcBaseAddress, use default value 
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
namespace Mtim.Grpc.Modbus.Services;

public class ModbusServiceFactory(IServiceProvider serviceProvider) : IModbusServiceFactory
{
    public IModbusService CreateModbusService(string protocol)
    {
        return protocol.ToLower() switch
        {
            "tcp" => serviceProvider.GetRequiredService<ModbusTcpService>(),
            "udp" => serviceProvider.GetRequiredService<ModbusUdpService>(),
            _ => throw new ArgumentException("Unsupported protocol", nameof(protocol)),
        };
    }
}
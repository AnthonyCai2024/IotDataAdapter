namespace Mtim.Grpc.Modbus.Services;

public interface IModbusServiceFactory
{
    IModbusService CreateModbusService(string protocol);
}
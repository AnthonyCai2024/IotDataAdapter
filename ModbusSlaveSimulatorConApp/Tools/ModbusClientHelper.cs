using NModbus;

namespace ModbusSlaveSimulatorConApp.Tools;

public static class ModbusClientHelper
{
    private const ushort StartAddress = 0;

    public static void WriteTasks(IModbusMaster master, byte count = 10)
    {
        // byte slaveId = 1;
        // var writeData = DataStoreHelper.CreateRandomData();
        // Console.WriteLine($"Writing to slave {slaveId}");
        // master.WriteMultipleRegisters(slaveId, StartAddress, writeData);

        for (byte i = 1; i < count + 1; i++)
        {
            Console.WriteLine($"Writing to slave {i}");
            var slaveId = i;
            var writeData = DataStoreHelper.CreateRandomData();
            master.WriteMultipleRegisters(slaveId, StartAddress, writeData);
            Task.Delay(200);
        }
    }
}
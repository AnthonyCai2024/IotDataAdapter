using NModbus;

namespace ModbusSlaveSimulatorConApp.Tools;

public static class SlaveHelper
{
    public static IEnumerable<IModbusSlave> CreateSlaves(ModbusFactory factory, byte count = 10)
    {
        var slaves = new List<IModbusSlave>();

        for (byte i = 1; i <= count; i++)
        {
            var slave = factory.CreateSlave(i);
            slaves.Add(slave);
        }

        return slaves;
    }
}
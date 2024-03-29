using System.Net;
using System.Net.Sockets;
using NModbus;

namespace ModbusSlaveSimulatorConApp.Tools;

public static class ModbusSlaveSimulator
{
    public static void Run(byte count = 10)
    {
        Console.WriteLine("Hello, modbus slave simulator!");

        const int port = 502;
        const ushort startAddress = 0;
        // IPAddress address = new IPAddress(new byte[] { 127, 0, 0, 1 });

        // create and start the TCP slave
        var slaveTcpListener = new TcpListener(IPAddress.Any, port);
        slaveTcpListener.Start();

        var factory = new ModbusFactory();

        IModbusSlaveNetwork network = factory.CreateSlaveNetwork(slaveTcpListener);

        // var slave1 = factory.CreateSlave(1);
        // var slave2 = factory.CreateSlave(2);

        var slaveList = SlaveHelper.CreateSlaves(factory, count);

        foreach (var slave in slaveList)
        {
            // add slaves to network
            network.AddSlave(slave);

            // create data store
            var writeDataStore = slave.DataStore;
            // writeDataStore.SyncRoot.EnterWriteLock();
            try
            {
                var writeData = DataStoreHelper.CreateRandomData();
                writeDataStore.HoldingRegisters.WritePoints(startAddress, writeData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        network.ListenAsync().GetAwaiter().GetResult();

        // prevent the main thread from exiting
        // Thread.Sleep(Timeout.Infinite);
    }
}
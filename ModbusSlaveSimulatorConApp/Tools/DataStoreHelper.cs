namespace ModbusSlaveSimulatorConApp.Tools;

public static class DataStoreHelper
{
    public static ushort[] CreateRandomData(int count = 1000)
    {
        // 生成随机值
        var random = new Random();

        var writeData = new ushort[count];
        for (var i = 0; i < count; i++)
        {
            // 分配随机值到地址
            writeData[i] = BitConverter.ToUInt16(BitConverter.GetBytes(random.Next()), 0);
        }

        return writeData;
    }
}
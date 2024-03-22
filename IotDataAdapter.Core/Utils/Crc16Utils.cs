namespace IotDataAdapter.Core.Utils;

public static class Crc16Utils
{
    public static byte[] Crc16(byte[] data)
    {
        var len = data.Length;
        if (len <= 0) return new byte[] { 0, 0 };
        ushort crc = 0xFFFF;

        for (var i = 0; i < len; i++)
        {
            crc = (ushort)(crc ^ (data[i]));
            for (var j = 0; j < 8; j++)
            {
                crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);
            }
        }

        var hi = (byte)((crc & 0xFF00) >> 8); //高位置
        var lo = (byte)(crc & 0x00FF); //低位置

        return new byte[] { hi, lo };
    }
}
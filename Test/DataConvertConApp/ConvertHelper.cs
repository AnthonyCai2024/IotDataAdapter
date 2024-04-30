namespace DataConvertConApp;

public class ConvertHelper
{
    public static void ConvertToSigned()
    {
        ushort rawVal = 65535;

        var bytes = BitConverter.GetBytes(rawVal); // 将 ushort 转换为字节数组
        // convert to signed integer ,use bytes
        var result = BitConverter.ToInt16(bytes, 0); // 将字节数组转换为 Int16


        Console.WriteLine($"rawVal:{rawVal},result:{result}");
    }

    public static void ConvertToBit()
    {
        ushort number = 272; // 你的ushort值
        
        string binary = Convert.ToString(number, 2).PadLeft(16, '0');
        
        Console.WriteLine($"ushort的二进制表示为: {binary}");
        
       
        int commBitIndex = 8; // 要计算的位索引

        int bitValue = (number >> commBitIndex) & 1;

        Console.WriteLine($"从右往左，位索引 {commBitIndex} 的值为: {bitValue}");
    }

    static int ReverseBits(int number)
    {
        int reversedNumber = 0;

        int numBits = sizeof(int) * 8;
        for (int i = 0; i < numBits; i++)
        {
            reversedNumber <<= 1;
            reversedNumber |= (number & 1);
            number >>= 1;
        }

        return reversedNumber;
    }
}
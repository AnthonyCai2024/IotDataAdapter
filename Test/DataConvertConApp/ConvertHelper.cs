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
    
    public static void ConvertToSignedBytes()
    {
        byte[] byteList = [1, 25];

        short signedInt16 = (short)((byteList[0] << 8) | byteList[1]);
        // ushort unsignedInt16 = (ushort)((byteList[1] << 8) | byteList[0]);
        Console.WriteLine($"Signed int16: {signedInt16}"); // Output: Signed int16: 280

        // Console.WriteLine($"signedInt16:{signedInt16}");
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

    public static void ConvertFloat32()
    {
        // 经过验证,Modbus poll中的float reverse就是 c# 中的float32 , 两者为倒转

        ushort value1 = 17258;
        ushort value2 = 13618;

        // 合并两个 16 位数为一个 32 位整数
        uint combinedValue = (uint)((value1 << 16) | value2);

        byte[] byteList = [67, 105, 187, 100];

        // 方法1: 使用 BitConverter
        var val1 = BitConverter.ToUInt16(byteList, 0);
        var val2 = BitConverter.ToUInt16(byteList, 2);

        ushort value1_manual = (ushort)((byteList[0] << 8) | byteList[1]);
        ushort value2_manual = (ushort)((byteList[2] << 8) | byteList[3]);

        /*
         val1 和 value1_manual 的结果会不同:
         *在 Little-Endian 系统上, BitConverter.ToUInt16(byteList, 0) 会将 byteList[0] 作为低字节, byteList[1] 作为高字节来构造 ushort 值。
         * 而手动转换 (ushort)((byteList[0] << 8) | byteList[1]) 则是将 byteList[0] 作为高字节, byteList[1] 作为低字节。

            所以在 Little-Endian 系统上, val1 和 value1_manual 的结果会不同:

            val1: 17223 (Little-Endian 字节序)
            value1_manual: 17224 (Big-Endian 字节序)
            同理, val2 和 value2_manual 也会有类似的差异。

            如果需要在不同字节序的系统之间进行数据交互,最好统一使用 Big-Endian 字节序,这样可以确保数据的正确性。手动转换的方式可以帮助你实现这一点。

            总之,这个差异是由于 BitConverter 方法会根据当前系统的字节序进行转换,而手动转换则可以控制字节序。理解这一点很重要,尤其是在涉及跨平台或网络传输的场景中。
         *
         *
         */

        ushort val1Reverse = (ushort)((byteList[1] << 8) | byteList[0]);

        // convert byte1,byte2 to ushort
        // 方法1: 使用 BitConverter
        byte[] bytes1 = BitConverter.GetBytes(value1);
        byte[] bytes2 = BitConverter.GetBytes(value2);


        uint combined2Value = (uint)((value1_manual << 16) | value2_manual);

        // 将 32 位整数转换为 float32
        float float32Value2 = BitConverter.ToSingle(BitConverter.GetBytes(combined2Value), 0);


        var float32ValueBytes = BitConverter.ToSingle(byteList, 0);

        // 将 32 位整数转换为 float32
        float float32Value = BitConverter.ToSingle(BitConverter.GetBytes(combinedValue), 0);

        // 反转字节顺序
        byte[] bytes = BitConverter.GetBytes(float32Value);
        Array.Reverse(bytes);
        float reversedFloat32Value = BitConverter.ToSingle(bytes, 0);

        Console.WriteLine($"Combined value (hex): {combinedValue:X8}");
        Console.WriteLine($"Float32 value: {float32Value}");
        Console.WriteLine($"float32Value2 value: {float32Value2}");
        Console.WriteLine($"Float32 value (reversed): {reversedFloat32Value}");
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
using IotDataAdapter.Core.Enum;

namespace IotDataAdapter.Core.Utils;

public abstract class HexCommandBuilder
{
    protected virtual byte[] HexBuild(int? slave, int? start, int length, int val, ModbusCommand command)
    {
        if (!slave.HasValue || !start.HasValue) return Array.Empty<byte>();
        var binSlave = Convert.ToByte(slave);

        // 发送命令：[设备地址] [命令号03] [起始寄存器地址高8位] [低8位] [读取的寄存器数高8位] [低8位] [CRC校验的低8位] [CRC校验的高8位]
        // 例：[01][03][00][6B][00][03][CRC低][CRC高]
        //
        // 写入:
        // 发送命令：[设备地址] [命令号06] [需下置的寄存器地址高8位] [低8位] [下置的数据高8位] [低8位] [CRC校验的低8位] [CRC校验的高8位]
        // 例：[01][06][00][01][00][03][CRC低][CRC高]


        var result = new List<byte>
        {
            //固定header
            0x0E,
            0x01,
            0x00,
            0x00,
            0x00,
            0x08,
            //设备地址
            binSlave,
            // 命令号
            (byte)command,
            // 起始寄存器地址高8位
            GetHigh8(start.Value),
            //低8位
            GetLow8(start.Value)
        };

        //读写不一样
        if (command.Equals(ModbusCommand.WriteSingle))
        {
            //下置的数据高8位
            result.Add(GetHigh8(val));
            //低8位
            result.Add(GetLow8(val));
        }
        else
        {
            //读取的寄存器数高8位
            result.Add(GetHigh8(length));
            //低8位
            result.Add(GetLow8(length));
        }

        //根据上述值计算crc校验
        var crcBytes = Crc16Utils.Crc16(result.ToArray());

        //crc 低8位在前
        result.Add(crcBytes[1]);

        //crc 高8位在后
        result.Add(crcBytes[0]);

        return result.ToArray();
    }

    protected virtual byte GetHigh8(int val)
    {
        /*
           //获取高8位
              int high8 = (num & 0xFF00) >> 8;
              //获取低8位
              int low8 = num & 0x00FF;
           */

        var high8 = (val & 0xFF00) >> 8;
        return Convert.ToByte(high8);
    }

    protected virtual byte GetLow8(int val)
    {
        var low8 = val & 0x00FF;

        return Convert.ToByte(low8);
    }

    protected virtual IEnumerable<int>? ShowByteData(byte[]? data)
    {
        //// 假设 data 是包含字节数组的数据
        //byte[] data = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C };

        if (data is not { Length: > 0 }) return default;
        // 定义起始位置和长度
        const int startIndex = 9; // 第10位
        var length = (data.Length - startIndex) / 2 * 2; // 确保长度是偶数

        Console.WriteLine($"data.Length:{data.Length},startIndex:{startIndex},length:{length}");

        // 从字节数组中提取数据
        var extractedData = new byte[length];
        Array.Copy(data, startIndex, extractedData, 0, length);

        Console.WriteLine($"extractedData.Length:{extractedData.Length}");

        // 将每两个字节组成一个数字
        var numbers = new int[length / 2];
        for (var i = 0; i < length; i += 2)
        {
            numbers[i / 2] = (extractedData[i] << 8) | extractedData[i + 1];
        }

        // 输出结果
        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }

        return numbers.ToList();
    }
}
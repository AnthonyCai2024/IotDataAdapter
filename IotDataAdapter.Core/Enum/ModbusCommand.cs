namespace IotDataAdapter.Core.Enum;

public enum ModbusCommand
{
    /// <summary>
    /// 读可读写数字量寄存器
    /// </summary>
    ReadDigital = 1,

    /// <summary>
    /// 读可读写模拟量寄存器（保持寄存器）,单个和多个都可以
    /// </summary>
    Read = 3,

    /// <summary>
    /// 写单个数字量（线圈状态）
    /// </summary>
    WriteDigital = 5,

    /// <summary>
    /// 写单个模拟量寄存器（保持寄存器）
    /// </summary>
    WriteSingle = 6,

    /// <summary>
    /// 写多个模拟量寄存器（保持寄存器）
    /// </summary>
    WriteMultiple = 16
}
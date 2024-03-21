namespace IotDataAdapter.Core.Models;

public class ModbusParameters
{
    public int Start { get; set; }
    public int Length { get; set; }
    public byte SlaveId { get; set; }
}
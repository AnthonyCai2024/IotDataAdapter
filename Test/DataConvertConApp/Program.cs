// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

ushort rawVal=65535;

var bytes = BitConverter.GetBytes(rawVal); // 将 ushort 转换为字节数组
// convert to signed integer ,use bytes
var result = BitConverter.ToInt16(bytes, 0); // 将字节数组转换为 Int16


Console.WriteLine($"rawVal:{rawVal},result:{result}");
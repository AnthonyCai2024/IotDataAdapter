using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace WebApplication1.Services;

public static class YamlHelper
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    private static readonly ISerializer Serializer = new SerializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    public static T ReadYaml<T>(string filePath)
    {
        var yamlContent = File.ReadAllText(filePath);
        return Deserializer.Deserialize<T>(yamlContent);
    }

    public static void WriteYaml<T>(string filePath, T data)
    {
        var yamlContent = Serializer.Serialize(data);
        File.WriteAllText(filePath, yamlContent);
    }

    // public static void Main(string[] args)
    // {
    //     string filePath = "config.yaml";
    //
    //     // 读取 YAML 文件
    //     var config = YamlHelper.ReadYaml<Config>(filePath);
    //     Console.WriteLine($"Setting1: {config.AppSettings.Setting1}");
    //     Console.WriteLine($"Setting2: {config.AppSettings.Setting2}");
    //
    //     // 修改配置并写入 YAML 文件
    //     config.AppSettings.Setting1 = "newValue1";
    //     config.AppSettings.Setting2 = 10;
    //     YamlHelper.WriteYaml(filePath, config);
    //
    //     // 再次读取 YAML 文件验证修改
    //     var updatedConfig = YamlHelper.ReadYaml<Config>(filePath);
    //     Console.WriteLine($"Updated Setting1: {updatedConfig.AppSettings.Setting1}");
    //     Console.WriteLine($"Updated Setting2: {updatedConfig.AppSettings.Setting2}");
    // }
}
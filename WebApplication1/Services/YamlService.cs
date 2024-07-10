using WebApplication1.Entity;

namespace WebApplication1.Services;

public class YamlService
{
    public static void LoadYaml()
    {
        string filePath = "config.yaml";

        var config = new Config();

        // 修改配置并写入 YAML 文件
        config.AppSettings.Version = "1.1.1";
        config.AppSettings.PublishDate = "2021-09-01";
        config.AppSettings.PublishLog = "Initial version";

        // config.AppSettings.Setting2 = 10;
        YamlHelper.WriteYaml(filePath, config);

        // 读取 YAML 文件
        // var config1 = YamlHelper.ReadYaml<Config>(filePath);
        // Console.WriteLine($"Setting1: {config.Setting1}");


        //
        // // 再次读取 YAML 文件验证修改
        // var updatedConfig = YamlHelper.ReadYaml<Config>(filePath);
        // Console.WriteLine($"Updated Setting1: {updatedConfig.AppSettings.Setting1}");
        // Console.WriteLine($"Updated Setting2: {updatedConfig.AppSettings.Setting2}");
    }
}
# IotDataAdapter

Data adapter for IOT

## project folder structure

```plaintext
/IotDataAdapter/
|-- /src/
|   |-- /IotDataAdapter.Core/                # 核心业务逻辑层
|   |   |-- IotDataAdapter.Core.csproj
|   |   |-- /Models/
|   |   |-- /Interfaces/
|   |   |-- /Services/
|   |-- /IotDataAdapter.Infrastructure/      # 基础设施层，如数据库访问、外部服务集成
|   |   |-- IotDataAdapter.Infrastructure.csproj
|   |   |-- /Data/
|   |   |-- /Repositories/
|   |   |-- /ExternalServices/
|   |-- /IotDataAdapter.Grpc/                # gRPC服务层
|   |   |-- IotDataAdapter.Grpc.csproj
|   |   |-- /Protos/
|   |   |-- /Services/
|   |-- /IotDataAdapter.Api/                 # Web API层
|   |   |-- IotDataAdapter.Api.csproj
|   |   |-- /Controllers/
|   |   |-- /Configurations/
|   |   |-- IotDataAdapter.sln     # 解决方案文件
|   |-- /IotDataAdapter.MqttClient/          # MQTT客户端项目
|   |   |-- IotDataAdapter.MqttClient.csproj
|   |   |-- /Services/                      # MQTT客户端服务
|   |   |-- /Controllers/                   # MQTT 客户端控制器
|-- /tests/                                 # 单元测试和集成测试
|   |-- /IotDataAdapter.Core.Tests/
|   |   |-- IotDataAdapter.Core.Tests.csproj
|   |   |-- /UnitTest/
|   |-- /IotDataAdapter.Infrastructure.Tests/
|   |   |-- IotDataAdapter.Infrastructure.Tests.csproj
|   |   |-- /IntegrationTest/
|   |-- /IotDataAdapter.Grpc.Tests/
|   |   |-- IotDataAdapter.Grpc.Tests.csproj
|   |   |-- /UnitTest/
|   |-- /IotDataAdapter.Api.Tests/
|   |   |-- IotDataAdapter.Api.Tests.csproj
|   |   |-- /IntegrationTest/
|-- .gitignore                              # Git 忽略文件
|-- README.md                               # 项目说明文档
|-- LICENSE                                 # 许可证文件

```

## 解释：

```plaintext
src/：包含所有源代码的目录。
IotDataAdapter.Core/：核心业务逻辑层，定义消息中心的核心功能和接口。
IotDataAdapter.Infrastructure/：基础设施层，包括数据访问代码、仓储和对外部服务的集成。
IotDataAdapter.Grpc/：gRPC服务层，包含服务定义（.proto）和服务实现。
IotDataAdapter.Api/：Web API层，提供RESTful API接口。
IotDataAdapter.MqttClient/：MQTT客户端项目，管理消息的发布和订阅。
IotDataAdapter.MqttBroker/：MQTT代理配置和管理。
tests/：包含所有测试代码的目录，为每个项目提供单元测试和集成测试。
.gitignore：Git忽略文件，用于排除不需要版本控制的文件（例如，编译输出和临时文件）。
README.md：项目的README文档，包括项目介绍、构建指南和使用说明。
LICENSE：项目的许可证文件。
IotDataAdapter.sln：Visual Studio解决方案文件，包含上述所有项目。
```

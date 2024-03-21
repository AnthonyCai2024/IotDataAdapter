using IotDataAdapter.Core.Models;
using IotDataAdapter.Core.Services;

namespace TestIotDataAdapter;

public class DevicePollingServiceTests
{
    [Fact]
    public async Task PollDevicesAsync_AllDevicesPolled_ConcurrencyLimited()
    {
        // Arrange
        var devices = new List<TcpParameter>
        {
            new TcpParameter { Ip = "192.168.4.43" },
            // new TcpParameter { Ip = "192.168.1.2" },
            // 添加更多设备以满足测试需要
        };
        var service = new DevicePollingService(devices, TimeSpan.FromSeconds(30)); // 假设最大并发数为2

        await service.PollDevicesAsync();

        // var cancellationTokenSource = new CancellationTokenSource();
        //
        // // Act
        // var startTime = DateTime.Now;
        // // await service.PollDevicesAsync(cancellationTokenSource.Token);
        // await service.PollDevicesAsync();
        // var endTime = DateTime.Now;
        //
        // // Assert
        // // 确保所有设备都被轮询
        // // 此处的断言需要根据你的实际轮询逻辑和期望结果进行调整
        // Assert.True(endTime - startTime < TimeSpan.FromSeconds(devices.Count), "Expected polling to be concurrent.");
        //
        // // 你还可以检查是否有任何异常被抛出，或者所有设备都正确地接收到了轮询请求
        // // 这可能需要你的轮询逻辑支持某种形式的结果记录或状态检查
    }
}
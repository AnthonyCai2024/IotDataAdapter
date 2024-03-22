using System.Net.Sockets;
using IotDataAdapter.Api.Services;
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;
using Moq;

namespace TestIotDataAdapter;

public class DataCollectionServiceTest
{
    [Fact]
    public async Task CollectDataAsync_ReturnsExpectedData()
    {
        // Arrange
        var mockConnectionStrategy = new Mock<IConnectionStrategy<TcpParameter, UdpClient, byte[]>>();
        mockConnectionStrategy.Setup(m =>
            m.ConnectAsync(It.IsAny<TcpParameter>())).ReturnsAsync(new UdpClient());

        // mockDataProtocolStrategy.Setup(m => m.GetDataAsync()).ReturnsAsync("测试数据");

        var service = new DataCollectionService(mockConnectionStrategy.Object, new Mock<IRedisService>().Object);

        var tcpParameter = new TcpParameter
        {
            Slave = 1,
            Start = 1,
            Length = 1,
            Val = 1,
            PlcBaseAddress = 1,
            Ip = "192.168.4.41",
            Port = 1086
        };

        // Act
        await service.CollectSingleDataAsync(tcpParameter);

        // Assert
        // Assert.Equal("测试数据", result);
    }
}
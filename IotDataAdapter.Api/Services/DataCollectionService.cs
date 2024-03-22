using System.Net.Sockets;
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Api.Services;

public class DataCollectionService(IConnectionStrategy<TcpParameter, UdpClient, byte[]> connectionStrategy)
    : IDataCollectionService
{
    public async Task CollectDataAsync(TcpParameter para)
    {
        var client = await connectionStrategy.ConnectAsync(para);

        var data = await connectionStrategy.SendAsync(client, para);
    }
}
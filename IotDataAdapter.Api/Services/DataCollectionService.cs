using System.Net.Sockets;
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Api.Services;

public class DataCollectionService(IConnectionStrategy<TcpParameter, UdpClient, byte[]> connectionStrategy)
    : IDataCollectionService
{
    public async Task CollectSingleDataAsync(TcpParameter para)
    {
        var client = await connectionStrategy.ConnectAsync(para);

        var data = await connectionStrategy.SendAsync(client, para);
    }

    public async Task CollectMultiDataAsync(List<TcpParameter> paras)
    {
        foreach (var para in paras)
        {
            try
            {
                await CollectSingleDataAsync(para);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }
        }
    }
    
    public async Task ParallelCollectMultiDataAsync(IEnumerable<TcpParameter> paras)
    {
        var tasks = paras.Select(CollectSingleDataAsync).ToList();
        await Task.WhenAll(tasks);
    }
}
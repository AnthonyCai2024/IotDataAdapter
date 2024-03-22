using System.Diagnostics;
using System.Net.Sockets;
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Api.Services;

public class DataCollectionService(IConnectionStrategy<TcpParameter, UdpClient, byte[]> connectionStrategy)
    : IDataCollectionService
{
    public async Task CollectSingleDataAsync(TcpParameter para)
    {
        try
        {
            var client = await connectionStrategy.ConnectAsync(para);

            var data = await connectionStrategy.SendAsync(client, para);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return;
        }
    }

    public async Task CollectMultiDataAsync(List<TcpParameter> paras)
    {
        Stopwatch sw = new();
        sw.Start();
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

        sw.Stop();
        Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
    }

    public async Task ParallelCollectMultiDataAsync(IEnumerable<TcpParameter> paras)
    {
        Stopwatch sw = new();
        sw.Start();
        var tasks = paras.Select(CollectSingleDataAsync).ToList();
        await Task.WhenAll(tasks);
        sw.Stop();
        Console.WriteLine($"Total time: {sw.ElapsedMilliseconds} ms");
    }
}
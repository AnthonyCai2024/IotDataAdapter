using System.Diagnostics;
using System.Net.Sockets;
using IotDataAdapter.Core.Interfaces;
using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Api.Services;

public class DataCollectionService(
    IConnectionStrategy<TcpParameter, UdpClient, byte[]> connectionStrategy,
    IRedisService redisService)
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
            // add this ip to redis
            await redisService.ListRightPushAsync("error", para.Ip);
        }
    }

    public async Task CollectMultiDataAsync(List<TcpParameter> paras)
    {
        Stopwatch sw = new();
        sw.Start();

        //get skip from redis
        var skipList = await redisService.ListRangeAsync("error");

        //filter skip ip
        var list = paras.Where(para => !skipList.Contains(para.Ip)).ToList();

        foreach (var para in list)
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
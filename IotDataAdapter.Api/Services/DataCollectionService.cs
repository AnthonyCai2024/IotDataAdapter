using System.Diagnostics;
using System.Net.Sockets;
using IotDataAdapter.Core.Config;
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
            // add this ip to redis, skip next time, 1 minute
            await redisService.StringSet(MtimConst.Redis.UnavailableIp + para.Ip
                , para.Ip,
                TimeSpan.FromMinutes(1));
        }
    }

    public async Task CollectMultiDataAsync(List<TcpParameter> paras)
    {
        Stopwatch sw = new();
        sw.Start();

        //get skip from redis
        var skipList = await redisService.GetKeysAsync(MtimConst.Redis.UnavailableIp + "*");

        //filter
        var filterList = paras.Where(para =>
        {
            var ip = skipList.FirstOrDefault(s => s.Split(":")[2] == para.Ip);
            return string.IsNullOrEmpty(ip);
        });

        foreach (var para in filterList)
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
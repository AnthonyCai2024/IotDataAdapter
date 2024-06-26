﻿using IotDataAdapter.Core.Interfaces;
using StackExchange.Redis;

namespace IotDataAdapter.Core.Services;

public class RedisService(IRedisConnection redisConnection, int databaseNumber) : IRedisService
{
    private IDatabase Database { get; } = redisConnection.Connection.GetDatabase(databaseNumber);


    // string get
    public async Task<RedisValue> StringGet(string key)
    {
        return await Database.StringGetAsync("key");
    }

    // string set
    public async Task StringSet(string key, string value, TimeSpan expiry = default)
    {
        await Database.StringSetAsync(key, value, expiry);
    }

    // list right push
    public async Task<long> ListRightPushAsync(string key, IEnumerable<string> list)
    {
        var redisValues = list.Select(value => (RedisValue)value).ToArray();
        return await Database.ListRightPushAsync(key, redisValues);
    }

    // right push
    public async Task<long> ListRightPushAsync(string key, string value)
    {
        return await Database.ListRightPushAsync(key, value);
    }

    // list range
    public async Task<List<string?>> ListRangeAsync(string key)
    {
        var redisValues = await Database.ListRangeAsync(key, 0, 100);
        return redisValues.Select(redisValue => (string?)redisValue).ToList();
    }

    // list length
    public async Task<long> ListLengthAsync(string key)
    {
        return await Database.ListLengthAsync(key);
    }

    // list remove
    public async Task ListRemoveAsync(string key)
    {
        await Database.ListTrimAsync(key, 100, -1);
    }

    // 根据pattern获取key
    public Task<List<string>> GetKeysAsync(string pattern)
    {
        var keys = new List<string>();
        var endpoints = redisConnection.Connection.GetEndPoints();
        foreach (var endpoint in endpoints)
        {
            var server = redisConnection.Connection.GetServer(endpoint);
            keys.AddRange(server.Keys(pattern: pattern).Select(key => key.ToString()));
        }

        return Task.FromResult(keys);
    }
}
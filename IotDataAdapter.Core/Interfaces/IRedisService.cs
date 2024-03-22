using StackExchange.Redis;

namespace IotDataAdapter.Core.Interfaces;

public interface IRedisService
{
    Task<RedisValue> StringGet(string key);

    // string set
    Task StringSet(string key, string value);


    // list right push
    Task<long> ListRightPushAsync(string key, IEnumerable<string> list);


    // list range
    Task<List<string?>> ListRangeAsync(string key);


    // list length
    Task<long> ListLengthAsync(string key);


    // list remove
    Task ListRemoveAsync(string key);
}
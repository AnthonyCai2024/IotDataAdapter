using StackExchange.Redis;

namespace IotDataAdapter.Core.Interfaces;

public interface IRedisConnection
{
    ConnectionMultiplexer Connection { get; }
}
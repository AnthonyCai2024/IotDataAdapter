using IotDataAdapter.Core.Interfaces;
using StackExchange.Redis;

namespace IotDataAdapter.Core.Services;

public class RedisConnection(string connectionString) : IRedisConnection
{
    public ConnectionMultiplexer Connection { get; private set; } = ConnectionMultiplexer.Connect(connectionString);
}
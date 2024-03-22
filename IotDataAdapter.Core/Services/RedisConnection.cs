using StackExchange.Redis;

namespace IotDataAdapter.Core.Services;

public class RedisConnection(string connectionString)
{
    public ConnectionMultiplexer Connection { get; private set; } = ConnectionMultiplexer.Connect(connectionString);
}
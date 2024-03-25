namespace IotDataAdapter.Core.Config;

public static class MtimConst
{
    public static class Redis
    {
        private const string Prefix = "mtim";
        public const string UnavailableIp = Prefix + ":unavailable_ip:";
    }
}
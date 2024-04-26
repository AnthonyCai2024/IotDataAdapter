namespace Mtim.ProtoHub.WebApp.Protocol;

public interface IProtocolStrategy
{
    /// <summary>
    /// write
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task WriteSingle(ICommandParameters parameters);
}
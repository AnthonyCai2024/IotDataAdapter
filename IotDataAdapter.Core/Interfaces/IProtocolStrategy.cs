namespace IotDataAdapter.Core.Interfaces;

public interface IProtocolStrategy<in TParameters, TResult>
{
    Task<TResult> ProcessDataAsync(TParameters parameters);
}
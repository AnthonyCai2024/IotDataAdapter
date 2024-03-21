namespace IotDataAdapter.Core.Interfaces;

public interface IConnectionStrategy<in TParameters>
{
    Task ConnectAsync(TParameters parameters);
}
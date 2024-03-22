using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Api.Services;

public interface IDataCollectionService
{
    Task CollectDataAsync(TcpParameter para);
}
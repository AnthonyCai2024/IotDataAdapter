using IotDataAdapter.Core.Models;

namespace IotDataAdapter.Api.Services;

public interface IDataCollectionService
{
    Task CollectSingleDataAsync(TcpParameter para);

    Task CollectMultiDataAsync(List<TcpParameter> paras);

    Task ParallelCollectMultiDataAsync(IEnumerable<TcpParameter> paras);
}
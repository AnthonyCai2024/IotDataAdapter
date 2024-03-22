using Microsoft.AspNetCore.Mvc;

namespace IotDataAdapter.Api.Controller;

[ApiController]
[Route("[controller]")]
public class DataCollectionController
{
    [HttpGet]
    public IEnumerable<int> Get()
    {
        return Enumerable.Range(1, 5)
            .ToArray();
    }
}
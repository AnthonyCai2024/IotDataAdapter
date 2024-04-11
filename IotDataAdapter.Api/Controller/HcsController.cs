using Microsoft.AspNetCore.Mvc;

namespace IotDataAdapter.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class HcsController
{
    [HttpPost("GetInfo")]
    public async Task CollectMultiDataAsync()
    {
        
    }
}
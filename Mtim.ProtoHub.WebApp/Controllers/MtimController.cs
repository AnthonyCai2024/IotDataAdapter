using Microsoft.AspNetCore.Mvc;
using Mtim.ProtoHub.WebApp.Protocol;

namespace Mtim.ProtoHub.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MtimController(IProtocolStrategy protocolStrategy)
{
    private readonly IProtocolStrategy _protocolStrategy = protocolStrategy;


    [HttpPost("WriteSingle")]
    public void WriteSingle([FromBody] ModbusTcpParameters para)
    {
        _protocolStrategy.WriteSingle(para);
    }
}
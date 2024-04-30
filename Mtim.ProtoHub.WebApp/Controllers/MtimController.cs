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
        //Tx:000082-5B D3 00 00 00 06 01 06 B7 AA 00 02
        
        _protocolStrategy.WriteSingle(para);
    }
}
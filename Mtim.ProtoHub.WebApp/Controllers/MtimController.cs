using Microsoft.AspNetCore.Mvc;

namespace Mtim.ProtoHub.WebApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MtimController
{
    [HttpGet("{command}")]
    public string ExecuteCommand(string command)
    {
        return command;
    }
}
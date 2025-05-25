using Azure.Core;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/debug")]
public class DebugController : ControllerBase
{
    [HttpGet("ver-header")]
    public IActionResult VerHeader()
    {
        bool existe = Request.Headers.TryGetValue("Cliente", out var valor);
        return Ok(new { existe, valor });
    }
}

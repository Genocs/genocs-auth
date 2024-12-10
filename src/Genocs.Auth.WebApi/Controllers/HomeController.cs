using Microsoft.AspNetCore.Mvc;

namespace Genocs.Auth.WebApi.Controllers;

[ApiController]
[Route("")]
public class HomeController : BaseController
{

    [HttpGet("")]
    public IActionResult Home()
    {
        return Ok("Genocs - Auth Web API.");
    }
}

namespace Genocs.Auth.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

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

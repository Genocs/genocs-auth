namespace Genocs.Auth.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class HomeController : BaseController
{

    public HomeController()
    {
    }

    [HttpGet("")]
    public IActionResult Home()
    {
        return Ok("Genocs - NET7 Auth Web API. Legal Disclaimer here...");
    }
}

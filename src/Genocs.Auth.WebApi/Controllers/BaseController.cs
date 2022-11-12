namespace Genocs.Auth.WebApi.Controllers;

using Genocs.Auth.Data.Entities;
using Microsoft.AspNetCore.Mvc;

[Controller]
public abstract class BaseController : ControllerBase
{
    // returns the current authenticated account (null if not logged in)
    public Account? Account => (Account?)HttpContext.Items["Account"];
}
using Genocs.Auth.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Genocs.Auth.WebApi.Controllers;

[Controller]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Returns the current authenticated account (null if not logged in).
    /// </summary>
    public Account? Account
        => (Account?)HttpContext.Items["Account"];
}
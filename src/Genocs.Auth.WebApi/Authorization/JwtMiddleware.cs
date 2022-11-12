namespace Genocs.Auth.WebApi.Authorization;

using Genocs.Auth.DataSqlServer;
using Genocs.Auth.WebApi.Helpers;
using Microsoft.Extensions.Options;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, SqlServerDbContext dataContext, IJwtUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrWhiteSpace(token))
        {

            var accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                // attach account to context on successful jwt validation
                context.Items["Account"] = await dataContext.Accounts.FindAsync(accountId.Value);
            }
        }

        await _next(context);
    }
}
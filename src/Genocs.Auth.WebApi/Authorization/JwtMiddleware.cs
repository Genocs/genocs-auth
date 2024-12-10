using Genocs.Auth.DataSqLite;
using Genocs.Auth.DataSqlServer;

namespace Genocs.Auth.WebApi.Authorization;

public class JwtMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context, SqLiteDbContext dataContext, IJwtUtils jwtUtils)
    {
        string? token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();
        if (!string.IsNullOrWhiteSpace(token))
        {
            int? accountId = jwtUtils.ValidateJwtToken(token);
            if (accountId != null)
            {
                // attach account to context on successful jwt validation
                context.Items["Account"] = await dataContext.Accounts.FindAsync(accountId.Value);
            }
        }

        await _next(context);
    }
}
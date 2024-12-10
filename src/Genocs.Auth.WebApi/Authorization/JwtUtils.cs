using Genocs.Auth.Data.Entities;
using Genocs.Auth.DataSqLite;
using Genocs.Auth.DataSqlServer;
using Genocs.Auth.WebApi.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Genocs.Auth.WebApi.Authorization;

public interface IJwtUtils
{
    public string GenerateJwtToken(Account account);
    public int? ValidateJwtToken(string? token);
    public RefreshToken GenerateRefreshToken(string? ipAddress);
}

public class JwtUtils(SqLiteDbContext context, IOptions<AppSettings> appSettings)
    : IJwtUtils
{
    private readonly SqLiteDbContext _context = context;
    private readonly AppSettings _appSettings = appSettings.Value;

    public string GenerateJwtToken(Account account)
    {
        // generate token that is valid for 15 minutes
        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("id", account.Id.ToString()),
                new Claim("role", account.Role.ToString()),
                new Claim("scope", "hardcoded_scope"),
                new Claim("admin_greetings", "hardcoded_scope", ClaimValueTypes.String),
                new Claim("admin_greetings", "full", ClaimValueTypes.String),
                new Claim("aud", "http://localhost:1012", ClaimValueTypes.String),
                new Claim("aud", "http://localhost:1013", ClaimValueTypes.String)]),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public int? ValidateJwtToken(string? token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            // return account id from JWT token if validation successful
            return int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }

    public RefreshToken GenerateRefreshToken(string? ipAddress)
    {
        var refreshToken = new RefreshToken
        {
            // token is a cryptographically strong random sequence of values
            Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),           
            Expires = DateTime.UtcNow.AddDays(7), // token is valid for 7 days
            Created = DateTime.UtcNow,
            CreatedByIp = ipAddress
        };

        // ensure token is unique by checking against db
        bool tokenIsUnique = !_context.Accounts.Any(a => a.RefreshTokens.Any(t => t.Token == refreshToken.Token));

        if (!tokenIsUnique)
            return GenerateRefreshToken(ipAddress);

        return refreshToken;
    }
}
namespace Genocs.Auth.Data.Entities;

public class Account
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Mobile { get; set; }
    public string PasswordHash { get; set; } = default!;
    public bool AcceptTerms { get; set; }
    public Role Role { get; set; }
    public string? VerificationToken { get; set; }
    public DateTime? EmailVerified { get; set; }
    public DateTime? MobileVerified { get; set; }

    public bool IsVerified => (EmailVerified.HasValue && MobileVerified.HasValue) || PasswordReset.HasValue;
    public string? ResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }
    public DateTime? PasswordReset { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public List<RefreshToken>? RefreshTokens { get; set; }

    public bool OwnsToken(string token)
    {
        return this.RefreshTokens?.Find(x => x.Token == token) != null;
    }

    public Account AddRefreshToken(RefreshToken refreshToken)
    {
        RefreshTokens ??= [];
        RefreshTokens.Add(refreshToken);

        return this;
    }

    public Account RemoveOldRefreshTokens(int days)
    {
        if (RefreshTokens == null) return this;

        RefreshTokens.RemoveAll(x =>
            !x.IsActive &&
            x.Created.AddDays(days) <= DateTime.UtcNow);

        return this;
    }
}
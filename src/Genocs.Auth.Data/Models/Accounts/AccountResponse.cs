namespace Genocs.Auth.Data.Models.Accounts;

public class AccountResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Mobile { get; set; }
    public string Role { get; set; } = default!;
    public DateTime? EmailVerified { get; set; }
    public DateTime? MobileVerified { get; set; }
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
    public bool IsVerified { get; set; }
}
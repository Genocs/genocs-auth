namespace Genocs.Auth.Data.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Password { get; set; } = default!;
}
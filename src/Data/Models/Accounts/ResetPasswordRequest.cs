namespace Genocs.Auth.Data.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public class ResetPasswordRequest
{
    [Required] public string Token { get; set; } = default!;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = default!;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = default!;
}
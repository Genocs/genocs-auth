namespace Genocs.Auth.Data.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public class ValidateResetTokenRequest
{
    [Required] public string Token { get; set; } = default!;
}
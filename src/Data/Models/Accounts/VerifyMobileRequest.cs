namespace Genocs.Auth.Data.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public class VerifyMobileRequest
{
    [Required]
    public string Code { get; set; } = default!;
}
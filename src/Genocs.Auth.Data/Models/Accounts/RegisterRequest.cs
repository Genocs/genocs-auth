namespace Genocs.Auth.Data.Models.Accounts;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Phone]
    public string? Mobile { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = default!;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = default!;

    [Range(typeof(bool), "true", "true")]
    public bool AcceptTerms { get; set; }
}
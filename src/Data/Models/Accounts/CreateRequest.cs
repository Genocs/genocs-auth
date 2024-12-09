namespace Genocs.Auth.Data.Models.Accounts;

using Genocs.Auth.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class CreateRequest
{
    [Required]
    public string Title { get; set; } = default!;

    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    [EnumDataType(typeof(Role))]
    public string Role { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = default!;

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = default!;
}
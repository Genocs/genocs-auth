namespace Genocs.Auth.WebApi.Configurations;

public class SmtpEmailSenderOptions
{
    public const string Position = "smtp";

    public string From { get; set; } = default!;
    public string Host { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}

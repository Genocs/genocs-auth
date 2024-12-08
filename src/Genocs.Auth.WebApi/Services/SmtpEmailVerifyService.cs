using Genocs.Auth.WebApi.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Genocs.Auth.WebApi.Services;

public interface IEmailVerifyService
{
    void Send(string to, string subject, string html, string? from = null);
}

public class SmtpEmailVerifyService(IOptions<SmtpEmailSenderOptions> smtpSettings) : IEmailVerifyService
{
    private readonly SmtpEmailSenderOptions _smtpSettings = smtpSettings.Value;

    public void Send(string to, string subject, string html, string? from = null)
    {
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _smtpSettings.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.StartTls);
        smtp.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
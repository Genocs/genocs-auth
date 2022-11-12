namespace Genocs.Auth.WebApi.Helpers;

public class AppSettings
{
    public static string Position { get; set; } = "AppSettings";
    public string Secret { get; set; }

    // refresh token time to live (in days), inactive tokens are
    // automatically deleted from the database after this time
    public int RefreshTokenTTL { get; set; }

    public string EmailFrom { get; set; }
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPass { get; set; }

    /// <summary>
    /// The twilio Account SID
    /// </summary>
    public string SmsAccountSid { get; set; } = default!;

    /// <summary>
    /// The Twilio Auth Token
    /// </summary>
    public string SmsAuthToken { get; set; } = default!;

    /// <summary>
    /// The Twilio ServiceId
    /// </summary>
    public string SmsServiceId { get; set; } = default!;

}
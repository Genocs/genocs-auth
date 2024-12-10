namespace Genocs.Auth.WebApi.Configurations;

public class TwilioOptions
{
    public const string Position = "twilio";

    /// <summary>
    /// The Twilio Account SID.
    /// </summary>
    public string SmsAccountSid { get; set; } = default!;

    /// <summary>
    /// The Twilio Auth Token.
    /// </summary>
    public string SmsAuthToken { get; set; } = default!;

    /// <summary>
    /// The Twilio ServiceId.
    /// </summary>
    public string SmsServiceId { get; set; } = default!;
}

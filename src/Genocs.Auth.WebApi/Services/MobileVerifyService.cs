namespace Genocs.Auth.WebApi.Services;

using Genocs.Auth.WebApi.Helpers;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

public interface IMobileService
{
    void Request(string phoneNumber);

    void Verify(string phoneNumber, string code);
}

public class MobileVerifyService : IMobileService
{
    private readonly AppSettings _appSettings;

    public MobileVerifyService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
        TwilioClient.Init(_appSettings.SmsAccountSid, _appSettings.SmsAuthToken);
    }

    public void Request(string phoneNumber)
    {
        var verification = VerificationResource.Create(
            to: phoneNumber,
            channel: "sms",
            pathServiceSid: _appSettings.SmsServiceId
        );

    }

    public void Verify(string phoneNumber, string code)
    {
        var verification = VerificationCheckResource.Create(
            to: phoneNumber,
            code: code,
            pathServiceSid: _appSettings.SmsServiceId
        );
    }
}
using Genocs.Auth.WebApi.Configurations;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Verify.V2.Service;

namespace Genocs.Auth.WebApi.Services;

public interface IMobileVerifyService
{
    void Request(string phoneNumber);
    void Verify(string phoneNumber, string code);
}

public class MobileVerifyService : IMobileVerifyService
{
    private readonly TwilioOptions _twilioOptions;

    public MobileVerifyService(IOptions<TwilioOptions> twilioOptions)
    {
        _twilioOptions = twilioOptions.Value;
        TwilioClient.Init(_twilioOptions.SmsAccountSid, _twilioOptions.SmsAuthToken);
    }

    public void Request(string phoneNumber)
    {
        _ = VerificationResource.Create(
            to: phoneNumber,
            channel: "sms",
            pathServiceSid: _twilioOptions.SmsServiceId);
    }

    public void Verify(string phoneNumber, string code)
    {
        _ = VerificationCheckResource.Create(
            to: phoneNumber,
            code: code,
            pathServiceSid: _twilioOptions.SmsServiceId);
    }
}
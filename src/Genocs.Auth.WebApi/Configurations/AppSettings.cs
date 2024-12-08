namespace Genocs.Auth.WebApi.Configurations;

public class AppSettings : Common.Configurations.AppOptions
{
    public string? Secret { get; set; }


    /// <summary>
    /// Refresh token time to live (in days), inactive tokens are
    /// automatically deleted from the database after this time
    /// </summary>
    public int RefreshTokenTTL { get; set; }

    /// <summary>
    /// It allows to skip the verification of the email and phone number
    /// </summary>
    public bool ValidUnverified { get; set; }
}
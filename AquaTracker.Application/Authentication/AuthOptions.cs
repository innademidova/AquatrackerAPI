namespace AquaTracker.Application.Authentication;

public static class AuthOptions
{
    public const string Issuer= "AquaTracker.Api";
    public const string Audience= "http://localhost:5000/";
    public const string Key= "mysupersecret_secretkey!123_for#AquaTracker";
    public static readonly TimeSpan Expiration = TimeSpan.FromMinutes(30);
}
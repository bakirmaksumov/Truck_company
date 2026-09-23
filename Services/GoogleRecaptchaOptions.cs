namespace Truck_company.Services;

public sealed class GoogleRecaptchaOptions
{
    public const string SectionName = "GoogleRecaptcha";

    public string SiteKey { get; set; } = string.Empty;
    public string SecretKey { get; set; } = string.Empty;
}

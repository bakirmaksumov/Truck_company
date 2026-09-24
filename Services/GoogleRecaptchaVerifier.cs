using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace Truck_company.Services;

public sealed class GoogleRecaptchaVerifier
{
    private readonly HttpClient _httpClient;
    private readonly GoogleRecaptchaOptions _options;
    private readonly ILogger<GoogleRecaptchaVerifier> _logger;

    public GoogleRecaptchaVerifier(HttpClient httpClient, IOptions<GoogleRecaptchaOptions> options, ILogger<GoogleRecaptchaVerifier> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<bool> VerifyAsync(string? token, string? remoteIp, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(_options.SecretKey) || string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        using var form = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["secret"] = _options.SecretKey,
            ["response"] = token,
            ["remoteip"] = remoteIp ?? string.Empty
        });

        using var response = await _httpClient.PostAsync(
            "https://www.google.com/recaptcha/api/siteverify",
            form,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return false;
        }

        var result = await response.Content.ReadFromJsonAsync<GoogleRecaptchaResponse>(cancellationToken);
        if (result?.Success != true)
        {
            _logger.LogWarning("reCAPTCHA verification rejected the login token. Error codes: {ErrorCodes}", string.Join(",", result?.ErrorCodes ?? []));
        }
        return result?.Success == true;
    }

    private sealed class GoogleRecaptchaResponse
    {
        public bool Success { get; set; }
        public string[] ErrorCodes { get; set; } = [];
    }
}

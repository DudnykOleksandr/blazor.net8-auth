using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace BlazorApp7.Controllers;

public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public static string ApiKeyHeaderName = "X-Api-Key";
    public const string ApiKeySchemeName = "ApiKey";

    private readonly string _apiKey;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IConfiguration configuration)
        : base(options, logger, encoder)
    {
        _apiKey = configuration.GetValue<string>("ApiKey") ?? string.Empty; // Зчитування API-ключа з конфігурації
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(ApiKeyHeaderName, out StringValues apiKeyHeaderValues))
        {
            return AuthenticateResult.NoResult(); // API-ключ не надано
        }

        var providedApiKey = apiKeyHeaderValues.FirstOrDefault();

        if (apiKeyHeaderValues.Count == 0 || string.IsNullOrWhiteSpace(providedApiKey))
        {
            return AuthenticateResult.NoResult(); // API-ключ порожній
        }

        if (providedApiKey == _apiKey)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "ApiKeyUser")
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        return AuthenticateResult.Fail("Invalid API key"); // API-ключ невірний
    }
}
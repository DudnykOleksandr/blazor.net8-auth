using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp7.Controllers;

[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.ApiKeySchemeName)]
[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

   
    [HttpGet("index")]
    public async Task<string> Index()
    {
        return "Ok";
    }

    [Authorize]
    [HttpPost("job")]
    public async Task<AccountJobDto?> GetJobAsync(string workerId, [FromQuery] Guid? jobId)
    {
        try
        {
            return new AccountJobDto();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Get job error");
        }

        return null;
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace BlazorApp7.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = ApiKeyAuthenticationHandler.ApiKeySchemeName)]

public class ElseController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;

    public ElseController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

   
    [HttpGet]
    public async Task<string> Index()
    {
        return "Ok";
    }



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

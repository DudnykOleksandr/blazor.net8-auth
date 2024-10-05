using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace BlazorApp7.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Login([FromForm] string userName="", [FromForm] string password="")
    {
        var claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, userName)); // add more claims

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(claimsIdentity);

        // Sign in the user
        await HttpContext.SignInAsync(principal);

        return Redirect($"/");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        // Sign in the user
        await HttpContext.SignOutAsync();

        return Redirect($"/");
    }
}

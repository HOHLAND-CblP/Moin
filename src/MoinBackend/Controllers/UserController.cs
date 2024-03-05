using Microsoft.AspNetCore.Mvc;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService service)
    {
        _userService = service;
    }

    public async Task Login(string username, string password, CancellationToken token)
    {
        var result = await _userService.Login(username, password, token);

        if (result)
            HttpContext.Response.Cookies.Append(".AspNetCore.Controllers.Method", "1234token",
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromDays(7),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                });
        else
        {
            HttpContext.Response.StatusCode = 400;
        }
    }
}
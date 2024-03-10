using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Responses;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;
using MoinBackend.Models;

namespace MoinBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    
    public UserController(IUserService service)
    {
        _userService = service;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<AuthenticateResponse> Login([FromBody]LoginModel loginModel, CancellationToken token)
    {
        var response = await _userService.Login(loginModel.Username, loginModel.Password, token);

        if (response != null)   
        {
            return response;
        }

        Response.StatusCode = 400;

        return null;
    }
}
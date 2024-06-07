using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Responses;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;
using MoinBackend.Models;

namespace MoinBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    private readonly IUserService _userService;
    
    public UserController(IUserService service)
    {
        _userService = service;
    }
    
    [HttpGet]
    [Route("CheckUsername")]
    public async Task<ActionResult<bool>> IsUsernameAvailable(string username, CancellationToken token)
    {
        return await _userService.IsUsernameAvailable(username, token);
    }
    
    
    [HttpPost]
    [Route("SignUp")]
    public async Task<ActionResult<AuthenticateResponse>> SignUp([FromBody] SignUpModel model, CancellationToken token)
    {
        User user = new User
        {
            Username = model.Username,
            Name = model.Name,
            Email = model.Email,
            Password = model.Password
        };
        var response = await _userService.SignUp(user, token);

        if (response != null)
        {
            return response;
        }
        
        return StatusCode(StatusCodes.Status401Unauthorized);
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<AuthenticateResponse>> Login([FromBody]LoginModel loginModel, CancellationToken token)
    {
        var response = await _userService.Login(loginModel.Username, loginModel.Password, token);

        if (response != null)   
        {
            return response;
        }

        return StatusCode(StatusCodes.Status401Unauthorized);
    }

    [Authorize]
    [HttpDelete]
    [Route("Delete")]
    public async Task<ActionResult> DeleteUser(CancellationToken token)
    {
        await _userService.DeleteUser(UserId, token);
        return Ok();
    }
}
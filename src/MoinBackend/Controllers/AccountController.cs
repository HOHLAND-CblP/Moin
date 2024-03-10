using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IAccountService _service;

    internal long UserId => long.Parse(User.FindFirst("sid").Value);

    public AccountController(IAccountService service)
    {
        _service = service;
    }
    
    [Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult<long>> Create([FromBody] Account account, CancellationToken token)
    {
        return await _service.Create(account, token);
    }
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<Account>> Get(long id, CancellationToken token)
    {
        var result = await _service.Get(id, token);
        if (result.UserId == UserId)
            return result;
        
        return null;
    }
}
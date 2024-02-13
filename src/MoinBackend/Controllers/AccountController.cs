using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController
{
    private readonly IAccountService _service;

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
        return await _service.Get(id, token);
    }
}
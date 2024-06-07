using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Controllers;

[ApiController]
//[ApiVersion("1.0")]
[Route("api/[controller]")]
public class AccountController : BaseController
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
        if (account.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);
            
        return await _service.Create(account, token);
    }
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<Account>> GetAccount(long id, CancellationToken token)
    {
        var result = await _service.Get(id, token);
        if (result == null)
            return NotFound();
        if (result.UserId == UserId)
            return result;
        
        return StatusCode(StatusCodes.Status403Forbidden);
    }
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<List<Account>>> GetAccounts(CancellationToken token)
    {
        var result = await _service.GetAccounts(UserId, token);
        
        return result;
    }

    
    /// <summary>
    /// Gets the note by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /note/D34D349E-43B8-429E-BCA4-793C932FD580
    /// </remarks>
    /// <param name="id">Note id (guid)</param>
    /// <returns>Returns NoteDetailsVm</returns>
    /// <response code="200">Success</response>
    /// <response code="401">If the user in unauthorized</response>
    [Authorize]
    [HttpDelete("[action]")]
    public async Task<ActionResult> Delete(long id, CancellationToken token)
    {
        var account = await _service.Get(id, token);

        if (account == null)
            return NotFound();
        
        if (account.UserId!=UserId)
            return StatusCode(StatusCodes.Status403Forbidden);
            
        await _service.Delete(id, token);
        return Ok();
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : BaseController
{
    private readonly ICurrencyService _service;

    public CurrencyController(ICurrencyService service)
    {
        _service = service;
    }
    
    /*[Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult<long>> Create([FromBody] Currency currency, CancellationToken token)
    {
        return await _service.Create(currency, token);
    }*/
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<Currency>> Get(long id, CancellationToken token)
    {
        var result = await _service.GetCurrency(id, token);
        
        return result;
    }
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<List<Currency>>> GetAll(CancellationToken token)
    {
        var result = await _service.GetAllCurrencies(token);
        
        return result;
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult> Delete(long id, CancellationToken token)
    {
        await _service.DeleteCurrency(id, token);   

        return Ok();
    }
}
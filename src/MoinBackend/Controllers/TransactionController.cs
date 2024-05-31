using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : BaseController
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpPost]
    public async Task<long> Create(Transaction transaction, CancellationToken token)
    {
        return await _service.Create(transaction, token);
    }

    [Authorize]
    [HttpGet]
    public async Task<Transaction> Get(long id, CancellationToken token)
    {
        return await _service.Get(id, token);
    }

    [Authorize]
    [HttpGet]
    public async Task<TransactionType> GetType(long id, CancellationToken token)
    {
        return await _service.GetType(id, token);
    }

    [Authorize]
    [HttpGet]
    public async Task<List<Transaction>> GetTransactions(long accountId, CancellationToken token)
    {
        return await _service.GetTransactions(accountId, token);
    }

    [Authorize]
    [HttpGet]
    public async Task Delete(long id, CancellationToken token)
    {
        await _service.Delete(id, token);
    }
}
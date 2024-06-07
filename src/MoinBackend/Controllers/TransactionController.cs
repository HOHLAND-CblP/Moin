using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : BaseController
{
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;

    public TransactionController(ITransactionService transactionService, IAccountService accountService)
    {
        _transactionService = transactionService;
        _accountService = accountService;
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult<long>> Create(Transaction transaction, CancellationToken token)
    {
        var account = await _accountService.Get(transaction.AccountId, token);
        if (account.UserId != UserId || transaction.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);
        
        return await _transactionService.Create(transaction, token);
    }
    
    [Authorize]
    [HttpPost("[action]")]
    public async Task<ActionResult<long>> CreateCategory(TransactionCategory category, CancellationToken token)
    {
        if (category.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);
            
        return await _transactionService.CreateCategory(category, token);
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<Transaction>> Get(long id, CancellationToken token)
    {
        var transaction = await _transactionService.Get(id, token);
        
        var account = await _accountService.Get(transaction.AccountId, token);
        if (account.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);

        return transaction;
    }
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<TransactionCategory>> GetCategory(long id, CancellationToken token)
    {
        var category = await _transactionService.GetCategory(id, token);
        
        if (category.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);

        return category;
    }
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<List<TransactionCategory>>> GetAllCategories(CancellationToken token)
    {
        var categories = await _transactionService.GetAllCategories(UserId, token);

        return categories;
    }

    /*[Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<TransactionType>> GetType(long id, CancellationToken token)
    {
        var transaction = await _transactionService.Get(id, token);
        if (transaction.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);

        return transaction.Type;
        //return await _transactionService.GetType(id, token);
    }*/

    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<List<Transaction>>> GetTransactions(long accountId, CancellationToken token)
    {
        var account = await _accountService.Get(accountId, token);
        if (account.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);
        
        return await _transactionService.GetTransactions(accountId, token);
    }

    [Authorize]
    [HttpDelete("[action]")]
    public async Task<ActionResult> Delete(long id, CancellationToken token)
    {
        var transaction = await _transactionService.Get(id, token);
        if (transaction.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);
        
        
        await _transactionService.Delete(id, token);
        return Ok();
    }
    
    [Authorize]
    [HttpDelete("[action]")]
    public async Task<ActionResult> DeleteCategory(long id, CancellationToken token)
    {
        var transaction = await _transactionService.Get(id, token);
        if (transaction.UserId != UserId)
            return StatusCode(StatusCodes.Status403Forbidden);
        
        
        await _transactionService.DeleteCategory(id, token);
        return Ok();
    }
}
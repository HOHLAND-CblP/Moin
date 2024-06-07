using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository repository)
    {
        _accountRepository = repository;
    }
    
    public async Task<long> Create(Account account, CancellationToken token)
    {
        account = new Account
        {
            UserId = account.UserId,
            Value = account.Value,
            CurrencyId = account.CurrencyId,
            CreationDate = DateTime.UtcNow
        };
        
        return await _accountRepository.Create(account, token);
    }
    

    public async Task<Account> Get(long id, CancellationToken token)
    {
        var account = await _accountRepository.GetAccount(id, token);

        return account;
    }

    public async Task<List<Account>> GetAccounts(long userId, CancellationToken token)
    {
        var accounts = await _accountRepository.GetAccounts(userId, token);

        return accounts;
    }

    public async Task Delete(long id, CancellationToken token)
    {
        await _accountRepository.Delete(id, token);
    }
}
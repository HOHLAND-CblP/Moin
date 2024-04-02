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
        return await _accountRepository.Create(account, token);
    }
    

    public async Task<Account> Get(long id, CancellationToken token)
    {
        var account = await _accountRepository.GetAccount(id, token);

        return account;
    }
}
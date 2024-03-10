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
    
    public Task<long> Create(Account account, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    

    public async Task<Account> Get(long id, CancellationToken token)
    {
        var account = await _accountRepository.GetAccount(id, token);

        return account;
    }
}
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task<long> Create(Account account, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Account> GetAccount(long id, CancellationToken token)
    {
        return Task.FromResult(new Account()
        {
            Id = 0,
            UserId = 1,
            Value = 10,
            CurrencyId = 0
        });
    }
}
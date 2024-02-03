using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Services;

public class AccountService : IAccountService
{
    public Task<long> Create(Account account, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<long> GetAccount(long id, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
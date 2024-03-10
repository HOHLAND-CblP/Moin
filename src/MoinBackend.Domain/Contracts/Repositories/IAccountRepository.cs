using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Repositories;

public interface IAccountRepository
{
    Task<long> Create(Account account, CancellationToken token);
    Task<Account> GetAccount(long id, CancellationToken token);
}
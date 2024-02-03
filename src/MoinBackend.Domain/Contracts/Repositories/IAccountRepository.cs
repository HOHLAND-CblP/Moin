using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Repositories;

public interface IAccountRepository
{
    Task<long> Create(Account account, CancellationToken token);
    Task<long> GetAccount(long id, CancellationToken token);
}
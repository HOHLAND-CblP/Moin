using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Services;

public interface IAccountService
{
    Task<long> Create(Account account, CancellationToken token);
    Task<long> GetAccount(long id, CancellationToken token);
    //Task<long> GetAccounts()
}
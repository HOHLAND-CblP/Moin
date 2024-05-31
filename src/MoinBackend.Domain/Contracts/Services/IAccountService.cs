using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Services;

public interface IAccountService
{
    Task<long> Create(Account account, CancellationToken token);
    Task<Account> Get(long id, CancellationToken token);
    Task<List<Account>> GetAccounts(long userId, CancellationToken token);
    Task Delete(long id, CancellationToken token);
    
}
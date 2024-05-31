using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Repositories;

public interface ITransactionRepository
{
    Task<long> Create(Transaction transaction, CancellationToken token);
    Task<Transaction> Get(long id, CancellationToken token);
    Task<TransactionType> GetType(long id, CancellationToken token);
    Task<List<Transaction>> GetTransactions(long accountId, CancellationToken token);
    Task Delete(long id, CancellationToken token);
}
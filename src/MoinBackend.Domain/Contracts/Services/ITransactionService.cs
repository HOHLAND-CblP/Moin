using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Services;

public interface ITransactionService
{
    Task<long> Create(Transaction transaction, CancellationToken token);
    Task<Transaction> Get(long id, CancellationToken token);
    Task<TransactionType> GetType(long id, CancellationToken token);
    Task<List<Transaction>> GetTransactions(long accountId, CancellationToken token);
    Task Delete(long id, CancellationToken token);
}
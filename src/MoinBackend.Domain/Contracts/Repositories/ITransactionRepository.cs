using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Repositories;

public interface ITransactionRepository
{
    Task<long> Create(Transaction transaction, CancellationToken token);
    Task<long> CreateCategory(TransactionCategory category, CancellationToken token);
    Task<Transaction> Get(long id, CancellationToken token);
    Task<TransactionCategory> GetCategory(long id, CancellationToken token);
    Task<List<TransactionCategory>> GetAllCategories(long userId, CancellationToken token);
    Task<TransactionType> GetType(long transactionId, CancellationToken token);
    Task<List<Transaction>> GetTransactions(long accountId, CancellationToken token);
    Task Delete(long id, CancellationToken token);
    Task DeleteCategory(long id, CancellationToken token);
}
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<long> Create(Transaction transaction, CancellationToken token)
    {
        transaction = new Transaction
        {
            AccountId = transaction.AccountId,
            Value = transaction.Value,
            Type = transaction.Type,
            CategoryId = transaction.CategoryId,
            CreationDate = DateTime.UtcNow
        };

        if (transaction.CategoryId % 2 != (int)transaction.Type)
            throw new ArgumentException($"Category does not apply to {transaction.Type}"); // надо проверить
            
        
        return await _repository.Create(transaction, token);
    }
    
    public async Task<long> CreateCategory(TransactionCategory category, CancellationToken token)
    {
        return await _repository.CreateCategory(category, token);
    }

    public async Task<Transaction> Get(long id, CancellationToken token)
    {
        return await _repository.Get(id, token);
    }

    public async Task<TransactionCategory> GetCategory(long id, CancellationToken token)
    {
        return await _repository.GetCategory(id, token);
    }

    public async Task<List<TransactionCategory>> GetAllCategories(long userId, CancellationToken token)
    {
        return await _repository.GetAllCategories(userId, token);
    }

    public async Task<TransactionType> GetType(long transactionId, CancellationToken token)
    {
        return await _repository.GetType(transactionId, token);
    }

    public async Task<List<Transaction>> GetTransactions(long accountId, CancellationToken token)
    {
        return await _repository.GetTransactions(accountId, token);
    }

    public async Task Delete(long id, CancellationToken token)
    {
        await _repository.Delete(id, token);
    }

    public async Task DeleteCategory(long id, CancellationToken token)
    {
        await _repository.DeleteCategory(id, token);
    }
}
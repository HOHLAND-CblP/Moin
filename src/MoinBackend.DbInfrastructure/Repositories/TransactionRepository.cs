using Dapper;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Infrastructure.Repositories;

public class TransactionRepository : PgRepository, ITransactionRepository
{
    public TransactionRepository(string connectionString) : base(connectionString) {}
    
    public async Task<long> Create(Transaction transaction, CancellationToken token)
    {
        string sql =
            $"""
             INSERT INTO transactions (account_id, type_id, value, creation_date)
                 VALUES (@AccountId, @TypeId, @Value, @CreationDate)
             return id;
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sql,
                new
                {
                    AccountId = transaction.AccountId,
                    TypeId = transaction.TypeId,
                    Value = transaction.Value,
                    CreationDate = transaction.CreationDate
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<Transaction> Get(long id, CancellationToken token)
    {
        string sql =
            $"""
             SELECT *
             FROM transactions
             WHERE @Id = id
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<Transaction>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<TransactionType> GetType(long transactionId, CancellationToken token)
    {
        string sql =
            $"""
             SELECT *
             FROM transaction_types tt
             JOIN transactions t ON (tt.id = t.type_id)
             WHERE @Id = t.id
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<TransactionType>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = transactionId
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<List<Transaction>> GetTransactions(long accountId, CancellationToken token)
    {
        string sql =
            $"""
             SELECT *
             FROM transactions
             WHERE @AccountId = account_id
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<Transaction>(
            new CommandDefinition(
                sql,
                new
                {
                    AccountId = accountId
                },
                cancellationToken: token))).ToList();
    }

    public async Task Delete(long id, CancellationToken token)
    {
        string sql =
            $"""
             DELETE FROM transactions
             WHERE @Id = id
             """;
        
        await using var connection = await GetConnection();
        
        await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: token));
    }

    
}
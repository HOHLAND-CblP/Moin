using Dapper;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;
using MoinBackend.Infrastructure.Settings;

namespace MoinBackend.Infrastructure.Repositories;

public class TransactionRepository : PgRepository, ITransactionRepository
{
    public TransactionRepository(IOptions<DbsOptions> dbsOptions) : base(dbsOptions.Value.PostgresConnectionString) {}
    
    public async Task<long> Create(Transaction transaction, CancellationToken token)
    {
        char sign = transaction.Type == TransactionType.Expense ? '-' : '+';
        string sql =
            $"""
             INSERT INTO transactions (account_id, value, type, category_id, creation_date)
                 VALUES (@AccountId, @Value, @Type, @CategoryId, @CreationDate)
             returning id;

             UPDATE accounts 
             SET value = value {sign} @Value
             WHERE id = @AccountId
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sql,
                new
                {
                    AccountId = transaction.AccountId,
                    Value = transaction.Value,
                    Type = transaction.Type.ToString(),
                    CategoryId = transaction.CategoryId,
                    CreationDate = transaction.CreationDate
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<long> CreateCategory(TransactionCategory category, CancellationToken token)
    {
        string next_id = category.Type == TransactionType.Expense
            ? "nextval('transaction_categories_expense_id_seq')"
            : "nextval('transaction_categories_income_id_seq')";
        
        string sql =
            $"""
             INSERT INTO transaction_categories (id, name, type, user_id)
                 VALUES ({next_id}, @Name, @Type, @UserId)
             returning id;
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sql,
                new
                {
                    Name = category.Name,
                    Type = category.Type.ToString(),
                    UserId = category.UserId
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

    public async Task<TransactionCategory> GetCategory(long id, CancellationToken token)
    {
        string sql =
            $"""
             SELECT *
             FROM transaction_categories
             WHERE @Id = id
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<TransactionCategory>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<List<TransactionCategory>> GetAllCategories(long userId, CancellationToken token)
    {
        string sql =
            $"""
             SELECT *
             FROM transaction_categories
             WHERE @UserId = user_id
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<TransactionCategory>(
            new CommandDefinition(
                sql,
                new
                {
                    UserId = userId
                },
                cancellationToken: token))).ToList();
    }

    public async Task<TransactionType> GetType(long transactionId, CancellationToken token)
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
                    Id = transactionId
                },
                cancellationToken: token))).FirstOrDefault().Type;
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

    public async Task DeleteCategory(long id, CancellationToken token)
    {
        string sql =
            $"""
             DELETE FROM transaction_categories
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
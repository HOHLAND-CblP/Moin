using Dapper;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;
using MoinBackend.Infrastructure.Settings;

namespace MoinBackend.Infrastructure.Repositories;

public class AccountRepository : PgRepository, IAccountRepository
{
    public AccountRepository(IOptions<DbsOptions> dbsOptions) : base(dbsOptions.Value.PostgresConnectionString)
    {}
    
    public async Task<long> Create(Account account, CancellationToken token)
    {
        string sql =
            $"""
            INSERT INTO accounts (userId, value, currency_id)
                VALUES (@UserId, @Value, @CurrencyId)
            return id;
            """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sql,
                new
                {
                    UserId = account.UserId,
                    Value = account.Value,
                    CurrencyId = account.CurrencyId
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<Account> GetAccount(long id, CancellationToken token)
    {
        string sql =
            """
            SELECT *
            FROM accounts
            WHERE @Id = id
            """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<Account>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<List<Account>> GetAccounts(long userId, CancellationToken token)
    {
        string sql =
            """
            SELECT *
            FROM accounts
            WHERE @Id = user_id
            """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<Account>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = userId
                },
                cancellationToken: token))).ToList();
    }

    public async Task Delete(long id, CancellationToken token)
    {
        string sql =
            """
            DELETE FROM accounts
            WHERE @Id=id;
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
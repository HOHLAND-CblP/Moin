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
            INSERT INTO account (userId, value, currency_id)
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

    public Task<Account> GetAccount(long id, CancellationToken token)
    {
        return Task.FromResult(new Account()
        {
            Id = 0,
            UserId = 1,
            Value = 10,
            CurrencyId = 0
        });
    }
}
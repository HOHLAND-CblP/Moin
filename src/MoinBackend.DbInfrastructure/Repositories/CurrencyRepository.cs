using Dapper;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;
using MoinBackend.Infrastructure.Settings;

namespace MoinBackend.Infrastructure.Repositories;

public class CurrencyRepository : PgRepository, ICurrencyRepository
{
    public CurrencyRepository(IOptions<DbsOptions> dbsOptions) : base(dbsOptions.Value.PostgresConnectionString)
    {
    }
    
    public async Task<long> Create(Currency currency, CancellationToken token)
    {
        string sql =
            $"""
             INSERT INTO currencies (name, currency_code, currency_symbol)
                 VALUES (@Name, @CurrencyCode, @CurrencySymbol)
             returning id;
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sql,
                new
                {
                    Name = currency.Name,
                    CurrencyCode = currency.CurrencyCode,
                    CurrencySymbol = currency.CurrencySymbol,
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<Currency> GetCurrency(long id, CancellationToken token)
    {
        string sql =
            $"""
             SELECT *
             FROM currencies
             WHERE @Id = id
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<Currency>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<List<Currency>> GetCurrencies(CancellationToken token)
    {
        string sql =
            $"""
             SELECT *
             FROM currencies;
             """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<Currency>(
            new CommandDefinition(
                sql,
                cancellationToken: token))).ToList();
    }

    public async Task DeleteCurrency(long id, CancellationToken token)
    {
        string sql =
            $"""
             DELETE FROM currencies
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
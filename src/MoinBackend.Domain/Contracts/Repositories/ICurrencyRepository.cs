using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Repositories;

public interface ICurrencyRepository
{
    Task<long> Create(Currency currency, CancellationToken token);
    Task<Currency> GetCurrency(long id, CancellationToken token);
    Task<List<Currency>> GetCurrencies(CancellationToken token);
    Task DeleteCurrency(long id, CancellationToken token);
}
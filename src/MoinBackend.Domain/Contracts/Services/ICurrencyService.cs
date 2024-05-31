using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Services;

public interface ICurrencyService
{
    Task<long> Create(Currency currency, CancellationToken token);
    Task<Currency> GetCurrency (long id, CancellationToken token);
    Task<List<Currency>> GetAllCurrencies(CancellationToken token);
    Task DeleteCurrency(long id, CancellationToken token);
}
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;

    public CurrencyService(ICurrencyRepository repository) => 
        _currencyRepository = repository;
    
    public async Task<long> Create(Currency currency, CancellationToken token)
    {
        var result = await _currencyRepository.Create(currency, token);
        return result;
    }

    public async Task<Currency> GetCurrency(long id, CancellationToken token)
    {
        var currency = await _currencyRepository.GetCurrency(id, token);
        return currency;
    }

    public async Task<List<Currency>> GetAllCurrencies(CancellationToken token)
    {
        var currencies = await _currencyRepository.GetCurrencies(token);
        return currencies;
    }

    public async Task DeleteCurrency(long id, CancellationToken token)
    {
        await _currencyRepository.DeleteCurrency(id, token);
    }
}
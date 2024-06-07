using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;
using MoinBackend.Domain.Services;
using MoinBackend.Domain.Settings;

namespace MoinBackend.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration config)
    { 
        services.Configure<AuthSettings>(config.GetSection("JWT"));
        
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<ITransactionService, TransactionService>();
        
        return services;
    }
}
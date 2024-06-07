using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Services;
using MoinBackend.Infrastructure.Infrastructure;
using MoinBackend.Infrastructure.Repositories;
using MoinBackend.Infrastructure.Settings;

namespace MoinBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDbInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DbsOptions>(config.GetSection("DataBases"));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        
        Postgres.MapCompositeTypes();
        Postgres.AddMigration(services, 
            services.BuildServiceProvider().GetService<IOptions<DbsOptions>>().Value.PostgresConnectionString);
        
        return services;
    }
}
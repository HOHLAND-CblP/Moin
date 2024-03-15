using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Infrastructure.Infrasructure;
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
        
        Postgres.MapCompositeTypes();
        Postgres.AddMigration(services, 
            services.BuildServiceProvider().GetService<IOptions<DbsOptions>>().Value.PostgresConnectionString);
        
        return services;
    }
}
using Microsoft.Extensions.DependencyInjection;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Infrastructure.Repositories;

namespace MoinBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        
        return services;
    }
}
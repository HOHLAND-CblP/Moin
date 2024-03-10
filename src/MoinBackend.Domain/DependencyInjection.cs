using System.Collections.Specialized;
using Microsoft.Extensions.DependencyInjection;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Services;
using MoinBackend.Domain.Settings;

namespace MoinBackend.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        
        return services;
    }
}
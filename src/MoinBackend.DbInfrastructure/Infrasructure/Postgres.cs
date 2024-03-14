using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace MoinBackend.Infrastructure.Infrasructure;

public class Postgres
{
    public static void AddMigration(IServiceCollection services, string connectionString)
    {
        services.AddFluentMigratorCore().ConfigureRunner(builder => builder.AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(Postgres).Assembly).For.Migrations());
    }
}
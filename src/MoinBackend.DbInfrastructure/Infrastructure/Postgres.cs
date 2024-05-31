using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace MoinBackend.Infrastructure.Infrastructure;

public class Postgres
{
    public static void MapCompositeTypes()
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public static void AddMigration(IServiceCollection services, string connectionString)
    {
        services.AddFluentMigratorCore().ConfigureRunner(builder => builder.AddPostgres()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(typeof(Postgres).Assembly).For.Migrations());
    }
}
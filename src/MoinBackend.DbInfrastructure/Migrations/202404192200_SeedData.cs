using FluentMigrator;

namespace MoinBackend.Infrastructure.Migrations;

[Migration(202404192200,"Seed data in to Currency table")]
public class SeedData : Migration
{
    public override void Up()
    {
        const string sql =
            """
            insert into currencies (id, name, currency_code, currency_symbol)
            values (1,    'Dollar USA', 'USD', '$')
                 , (2,          'Euro', 'EUR', '€')
                 , (3, 'Russian Ruble', 'RUB', '₽')
            """;
    }

    public override void Down()
    {
        Delete.FromTable("currencies")
            .AllRows();
    }
}
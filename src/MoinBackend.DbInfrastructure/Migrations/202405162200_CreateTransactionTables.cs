using FluentMigrator;

namespace MoinBackend.Infrastructure.Migrations;

[Migration(202405162200,"Create transactions and transaction_types tables")]
public class CreateTransactionTables : Migration
{
    public override void Up()
    {
        string sql =
            """
            CREATE TABLE IF NOT EXISTS transaction_categories (   
                id      bigserial PRIMARY KEY,
                name    varchar NOT NULL
            );
            
            CREATE TABLE IF NOT EXISTS transactions (   
                id              bigserial PRIMARY KEY,
                account_id      bigint NOT NULL,
                value           decimal NOT NULL,
                type            varchar NOT NULL,
                category_id     bigint NOT NULL,
                creation_date   timestamp with time zone NOT NULL default (now() at time zone 'utc' ),
            );
            """;

        Execute.Sql(sql);
    }

    public override void Down()
    {
        string sql =
            """
            DROP TABLE transactions;
            DROP TABLE transaction_types;
            """;
        
        Execute.Sql(sql);
    }
}
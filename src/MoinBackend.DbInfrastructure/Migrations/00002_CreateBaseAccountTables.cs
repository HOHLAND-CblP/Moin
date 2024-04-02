using FluentMigrator;

namespace MoinBackend.Infrastructure.Migrations;

[Migration(202404021610,"Create accounts and currency tables")]
public class CreateAccounts : Migration
{
    public override void Up()
    {
        /*Create.Table("currencies")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("name").AsString().NotNullable()
            .WithColumn("currency_code").AsString().NotNullable()
            .WithColumn("currency_symbol").AsCustom("\"char\"").NotNullable();*/

        const string sql =
            """
            CREATE TABLE IF NOT EXISTS currencies (
              id                serial PRIMARY KEY,
              name              varchar NOT NULL,
              currency_code     varchar NOT NULL,
              currency_symbol   char NOT NULL
            );


            """;

        Execute.Sql(sql);

        Create.Table("accounts")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("user_id").AsInt64().ForeignKey("users", "id").NotNullable()
            .WithColumn("value").AsDecimal().NotNullable()
            .WithColumn("currency_id").AsInt32().ForeignKey("currencies", "id").NotNullable();
    }

    public override void Down()
    {
        
        const string sql =
            """
            DROP TABLE accounts;
            DROP TABLE currencies;
            """;
        
        Execute.Sql(sql);
    }
}
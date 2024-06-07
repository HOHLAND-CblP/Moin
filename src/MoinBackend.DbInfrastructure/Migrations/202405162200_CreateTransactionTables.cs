using FluentMigrator;

namespace MoinBackend.Infrastructure.Migrations;

[Migration(202405162200,"Create transaction_categories table and transactions table")]
public class CreateTransactionTables : Migration
{
    public override void Up()
    {
        string sql =
            """
            CREATE TABLE IF NOT EXISTS transaction_categories (   
                id      bigint PRIMARY KEY,
                name    varchar NOT NULL,
                type    varchar NOT NULL,
                user_id bigint NOT NULL references users(id)
                CONSTRAINT type_check CHECK (id%2=0 AND type='Income' OR id%2=1 AND type='Expense')                                                       
            );
            
            CREATE SEQUENCE IF NOT EXISTS transaction_categories_expense_id_seq 
                AS BIGINT
                INCREMENT 2
                START 1
                OWNED BY transaction_categories.id;

            CREATE SEQUENCE IF NOT EXISTS transaction_categories_income_id_seq
                AS BIGINT
                INCREMENT 2
                START 2
                OWNED BY transaction_categories.id;

            CREATE TABLE IF NOT EXISTS transactions (   
                id              bigserial PRIMARY KEY,
                account_id      bigint NOT NULL references accounts(id),
                value           decimal NOT NULL CONSTRAINT positive_value CHECK (value > 0),
                type            varchar NOT NULL,
                category_id     bigint NOT NULL references transaction_categories(id),
                creation_date   timestamp with time zone NOT NULL default (now() at time zone 'utc'),
                CONSTRAINT type_check CHECK (category_id%2=0 AND type='Income' OR category_id%2=1 AND type='Expense')
            );
            """;

        Execute.Sql(sql);
    }

    public override void Down()
    {
        string sql =
            """
            DROP TABLE transactions;
            DROP TABLE transaction_categories;
            """;
        
        Execute.Sql(sql);
    }
}
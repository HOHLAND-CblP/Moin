using FluentMigrator;

namespace MoinBackend.Infrastructure.Migrations;

[Migration(00001,"InitMigration")]
public class InitSchema : Migration
{
    public override void Up()
    {
        const string sql = 
            """
            CREATE TABLE IF NOT EXISTS users (
                id                  bigserial PRIMARY KEY,
                username            varchar NOT NULL UNIQUE,
                name                varchar NOT NULL,
                email               varchar NOT NULL,
                password            varchar NOT NULL,
                creation_date       timestamp with time zone NOT NULL default (now() at time zone 'utc' ),
                last_update_date    timestamp with time zone NULL
            );            
            """;
        
        Execute.Sql(sql);
    }

    public override void Down()
    {
        const string sql =
            """
            DROP TABLE users;
            """;
        
        Execute.Sql(sql);
    }
}
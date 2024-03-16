using Dapper;
using Microsoft.Extensions.Options;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;
using MoinBackend.Infrastructure.Settings;

namespace MoinBackend.Infrastructure.Repositories;

public class UserRepository : PgRepository, IUserRepository
{
    public UserRepository(IOptions<DbsOptions> dbOptions) : base(dbOptions.Value.PostgresConnectionString)
    {}
    
    public async Task<long> Create(User user, CancellationToken token)
    {
        List<User> oo = new List<User>();

        oo.Where(u => u.Username == user.Username);
        
            string sql =
                """
                INSERT INTO users (username, name, email, password, creation_date)
                    VALUES (@Username, @Name, @Email, @Password, @CreationDate) 
                returning id;
            """;
        
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<long>(
            new CommandDefinition(
                sql,
                new
                {
                    Username = user.Username,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    CreationDate = user.CreationDate
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task<User> GetUserByUsername(string username, CancellationToken token)
    {
        string sql =
            """
            SELECT *
            FROM users
            WHERE username = @Username
            """;
        
        await using var connection = await GetConnection();
        
        return (await connection.QueryAsync<User>(
            new CommandDefinition(
                sql,
                new
                {
                    Username = username
                },
                cancellationToken: token))).FirstOrDefault();
    }

    public async Task DeleteUser(long id, CancellationToken token)
    {
        string sql =
            """
            DELETE FROM users
            WHERE @Id=id;
            """;
        
        await using var connection = await GetConnection();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id
                },
                cancellationToken: token));
    }
}
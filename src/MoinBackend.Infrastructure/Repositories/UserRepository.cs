using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Infrastructure.Repositories;

public class UserRepository : PgRepository, IUserRepository
{
    public UserRepository(string connectionString) : base(connectionString)
    {}
    
    public Task<long> Create(User user, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserByUsername(string username, CancellationToken token)
    {
        
        return Task.FromResult(new User()
        {
            Id = 0,
            Username = "hohladn_cblp",
            Email = "ismail987654321lkjhgfdsa@gmail.com",
            Name = "Ismail",
            Password = "XM66q2yxUTdH/W6A8M9AuEgyUg28ozx4SX+oM5QBwd5u+3i1idIrOI/u8e6QlNhXonCtdn2cc1a0b2QCN2Q0XMWOBOUb+HsJuDrImWTk/K39/6WeYl06V/UVQYtHUGp30PDD1J10IuKaNJ23tcpGg4u5HaUcLIR+Ag+63o8cqow="
        });
    }
}
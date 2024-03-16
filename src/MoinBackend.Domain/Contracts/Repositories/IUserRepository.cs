using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Repositories;

public interface IUserRepository
{
    Task<long> Create(User user, CancellationToken token);
    Task<User> GetUserByUsername(string username, CancellationToken token);
    Task DeleteUser(long id, CancellationToken token);
}
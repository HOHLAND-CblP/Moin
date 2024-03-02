using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByUsername(string username, CancellationToken token);
}
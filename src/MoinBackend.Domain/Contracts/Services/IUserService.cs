using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Services;

public interface IUserService
{
    Task<bool> IsUsernameBusy(string username, CancellationToken token);   
    Task<long> SignUp(User user, CancellationToken token);
    Task<bool> Login(string username, string password, CancellationToken token);
}
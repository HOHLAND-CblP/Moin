using MoinBackend.Domain.Contracts.Responses;
using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Services;

public interface IUserService
{
    Task<bool> IsUsernameBusy(string username, CancellationToken token);   
    Task<AuthenticateResponse> SignUp(User user, CancellationToken token);
    Task<AuthenticateResponse> Login(string username, string password, CancellationToken token);
}
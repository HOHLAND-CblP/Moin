using MoinBackend.Domain.Contracts.Services;

namespace MoinBackend.Domain.Services;

public class UserService : IUserService
{
    public Task<bool> IsLoginBusy(string login, CancellationToken token)
    {
        
        
        return Task.FromResult(true);
    }

    public Task SignUp(string login, string password, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task Login(string login, string password, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
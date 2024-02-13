namespace MoinBackend.Domain.Contracts.Services;

public interface IUserService
{
    Task<bool> IsLoginBusy(string login, CancellationToken token);   
    Task SignUp(string login, string password, CancellationToken token);
    Task Login(string login, string password, CancellationToken token);
}
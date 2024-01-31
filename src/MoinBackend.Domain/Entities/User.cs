namespace MoinBackend.Domain.Entities;

public class User
{
    public long Id { get; init; }
    public string UserName { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
}
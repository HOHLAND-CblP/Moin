using MoinBackend.Domain.Entities;

namespace MoinBackend.Domain.Contracts.Responses;

public class AuthenticateResponse
{
    public User User { get; init; }
    public string Token { get; init; }
}
namespace MoinBackend.Domain.Entities;

public class Account
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public decimal Value { get; init; }
}
namespace MoinBackend.Domain.Entities;

public class Transaction
{
    public long Id { get; init; }
    public long AccountId { get; init; }
    public decimal Value { get; init; }
    public TransactionType Type { get; init; }
    public long CategoryId { get; init; }
    public long UserId { get; init; }
    public DateTime CreationDate { get; init; }
}
namespace MoinBackend.Domain.Entities;

public class Transaction
{
    public long Id { get; init; }
    public long AccountId { get; init; }
    public TransactionType Type { get; init; }
    public decimal Value { get; init; }
    public DateTime CreationDate { get; init; }
}
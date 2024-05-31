namespace MoinBackend.Domain.Entities;

// категории с четным ID - это доходы, с нечетным - расходы
// categories with even ID are income, with odd ID are expenses
public class TransactionCategory
{
    public long Id { get; init; }
    public string Name { get; init; }
    public TransactionType Type { get; init; }
}
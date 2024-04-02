namespace MoinBackend.Domain.Entities;

public class Currency
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string CurrencyCode { get; init; }
    public char CurrencySymbol { get; init; }
}
using System.Text.Json.Serialization;

namespace MoinBackend.Domain.Entities;

public class User
{
    public long Id { get; init; }
    public string Username { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    [JsonIgnore]
    public string Password { get; init; }
}
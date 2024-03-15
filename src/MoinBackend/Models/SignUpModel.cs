using System.ComponentModel.DataAnnotations;

namespace MoinBackend.Models;

public class SignUpModel
{
    [Required]
    public string Username { get; init; }
    [Required]
    public string Name { get; init; }
    [Required]
    public string Email { get; init; }
    [Required]
    public string Password { get; init; }
}
using System.ComponentModel.DataAnnotations;

namespace MoinBackend.Models;

public class LoginModel
{
    [Required]
    public string Username { get; init; }
    [Required]
    public string Password { get; init; }
}
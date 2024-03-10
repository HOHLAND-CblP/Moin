using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MoinBackend.Domain.Settings;

public class AuthSettings
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
    public long TokenLifeTimeInSeconds { get; init; }

    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }
}
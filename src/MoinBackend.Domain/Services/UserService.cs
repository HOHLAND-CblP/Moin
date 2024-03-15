using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Contracts.Responses;
using MoinBackend.Domain.Contracts.Services;
using MoinBackend.Domain.Entities;
using MoinBackend.Domain.Settings;

namespace MoinBackend.Domain.Services;

public class UserService : IUserService
{
    private const int SaltLength = 64;
    private IUserRepository _userRepository;
    private readonly AuthSettings _authSettings;
    
    public UserService(IUserRepository repository, IOptions<AuthSettings> authSettings)
    {
        _userRepository = repository;
        _authSettings = authSettings.Value;
    }
    
    public async Task<bool> IsUsernameBusy(string username, CancellationToken token)
    {
        if (await _userRepository.GetUserByUsername(username, token) == null)
        {
            return true;
        }
        
        return false;
    }

    public async Task<AuthenticateResponse> SignUp(User signupUser, CancellationToken token)
    {
        string hashPassword = CreatePbkdf2Hash(signupUser.Password);

        User user = new User()
        {
            Username = signupUser.Username,
            Email = signupUser.Email,
            Name = signupUser.Name,
            Password = hashPassword,
            CreationDate = DateTime.UtcNow
        };

        var userId = await _userRepository.Create(user, token);
        AuthenticateResponse response = new AuthenticateResponse()
        {
            User = new User
            {
                Id = userId,
                Username = user.Username,
                Email = user.Email,
                Name = user.Name,
                CreationDate = user.CreationDate
            },
            Token = GenerateJWT(user)
                
        };

        return response;
    }
    
    public async Task<AuthenticateResponse> Login(string username, string password, CancellationToken token)
    {
        var user = await _userRepository.GetUserByUsername(username, token);

        

        if (user != null && CheckPassword(password, user.Password))
            return new AuthenticateResponse()
            {
                User = user,
                Token = GenerateJWT(user)
            };
        
        return null;
    }

    public async Task DeleteUser(string username, CancellationToken token)
    {
        
    }





    private string GenerateJWT(User user)
    {

        var credentials = new SigningCredentials(_authSettings.GetSymmetricSecurityKey(),
            SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            new Claim("Username", user.Username),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            _authSettings.Issuer,
            _authSettings.Audience,
            claims,
            expires:DateTime.UtcNow.AddSeconds(_authSettings.TokenLifeTimeInSeconds),
            signingCredentials:credentials);
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    
    
    private string CreatePbkdf2Hash(string password)
    {
        byte[] salt = GenerateSalt();
        string hashPassword = MakePbkdf2HashPassword(password, salt);
        
        return hashPassword;
    }

    
    private bool CheckPassword(string password, string hash)
    {
        byte[] salt = GetSalt(hash);

        string hashPassword = MakePbkdf2HashPassword(password, salt);

        return hash == hashPassword;
    }

    private byte[] GetSalt(string hash)
    {
        byte[] hashBytes = Convert.FromBase64String(hash);
        byte[] salt = new byte[SaltLength];

        for (int i = 0; i < SaltLength; i++)
            salt[i] = hashBytes[hashBytes.Length - SaltLength + i];
        
        return salt;
    }
    
    
    private string MakePbkdf2HashPassword(string password, byte[] salt)
    {
        byte[] passwordBytes  = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

        for (int i = 0; i < passwordBytes.Length; i++) 
            saltedPassword[i] = passwordBytes[i];

        for (int i = passwordBytes.Length; i < saltedPassword.Length; i++)
            saltedPassword[i] = salt[i - passwordBytes.Length];
        

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, salt, 1000, HashAlgorithmName.SHA512, 64);
        byte[] hashWithSaltBytes = new byte[hash.Length + salt.Length];
        
        for (int i = 0; i < hash.Length; i++) 
            hashWithSaltBytes[i] = hash[i];

        for (int i = hash.Length; i < hashWithSaltBytes.Length; i++)
            hashWithSaltBytes[i] = salt[i - hash.Length];


        
        return Convert.ToBase64String(hashWithSaltBytes);
    }

    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[SaltLength];
        
        var rngRand = new RNGCryptoServiceProvider();
        rngRand.GetBytes(salt);

        return salt;
    }
}
using System.Security.Cryptography;
using System.Text;
using MoinBackend.Domain.Contracts.Repositories;
using MoinBackend.Domain.Contracts.Services;

namespace MoinBackend.Domain.Services;

public class UserService : IUserService
{
    private const int SaltLenght = 64;
    private IUserRepository _userRepository;

    public UserService()
    {
        
    }
    
    public UserService(IUserRepository repository) =>
        _userRepository = repository;
    
    public async Task<bool> IsUsernameBusy(string username, CancellationToken token)
    {
        if (await _userRepository.GetUserByUsername(username, token) == null)
        {
            return true;
        }
        
        return false;
    }

    public Task SignUp(string login, string password, CancellationToken token)
    {
        /*string hashPassword = MakePbkdf2HashPassword(password);
        
        Console.WriteLine(hashPassword);*/
        return Task.CompletedTask;
    }
    
    public Task Login(string login, string password, CancellationToken token)
    {
        
        return Task.CompletedTask;
    }

    /*public string CreateHash(string password)
    {
        byte[] salt = GenerateSalt();
        string hashPassword = MakePbkdf2HashPassword(password, salt);
        
        return hashPassword;
    }

    
    public bool CheckPassword(string password, string hash)
    {
        byte[] salt = GetSalt(hash);

        string hashPassword = MakePbkdf2HashPassword(password, salt);

        return hash == hashPassword;
    }*/

    private byte[] GetSalt(string hash)
    {
        byte[] hashBytes = Convert.FromBase64String(hash);
        byte[] salt = new byte[SaltLenght];

        for (int i = 0; i < SaltLenght; i++)
            salt[i] = hashBytes[hashBytes.Length - SaltLenght + i];
        
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
        byte[] salt = new byte[SaltLenght];
        
        var rngRand = new RNGCryptoServiceProvider();
        rngRand.GetBytes(salt);

        return salt;
    }
}
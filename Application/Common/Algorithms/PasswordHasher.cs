using System.Security.Cryptography;
using System.Text;
using Application.Common.Interfaces.IAlgorithms;
using Application.Common.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common.Algorithms;

public class PasswordHasher : IPasswordHasher
{
    private readonly byte[] _key;

    public PasswordHasher(IConfiguration config)
    {
        var key = config["PasswordHasher:SecretKey"] ?? throw new ArgumentNullException(nameof(config), ErrorMessages.SecretKeyNotFoundError);
        
        Console.WriteLine(key);
        
        _key = Encoding.UTF8.GetBytes(key);
    }

    public string HashPassword(string password)
    {
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password), ErrorMessages.PasswordError);
        }

        using (var hmac = new HMACSHA256(_key))
        {
            var salt = GenerateSalt(16);
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltedPassword = new byte[salt.Length + passwordBytes.Length];

            Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

            var hash = hmac.ComputeHash(saltedPassword);

            var result = new byte[salt.Length + hash.Length];
            
            Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
            Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);

            return Convert.ToBase64String(result);
        }
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        if (hashedPassword == null)
        {
            throw new ArgumentNullException(nameof(hashedPassword), ErrorMessages.HashedPasswordError);
        }
        
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password), ErrorMessages.PasswordError);
        }

        var hashBytes = Convert.FromBase64String(hashedPassword);

        var salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, salt.Length);

        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = new byte[salt.Length + passwordBytes.Length];

        Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

        using (var hmac = new HMACSHA256(_key))
        {
            var computedHash = hmac.ComputeHash(saltedPassword);

            var storedHash = new byte[computedHash.Length];
            Buffer.BlockCopy(hashBytes, salt.Length, storedHash, 0, storedHash.Length);

            return ByteArraysEqual(computedHash, storedHash);
        }
    }

    private static byte[] GenerateSalt(int length)
    {
        var salt = new byte[length];
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        
        return salt;
    }

    private static bool ByteArraysEqual(byte[] b1, byte[] b2)
    {
        if (b1 == b2) return true;
        if (b1.IsNullOrEmpty() || b2.IsNullOrEmpty()) return false;
        if (b1.Length != b2.Length) return false;

        return !b1.Where((t, i) => t != b2[i]).Any();
    }
}
using System.Security.Cryptography;
using System.Text;
using Application.Common.Utils;
using Domain.Interfaces.IAlgorithms;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Algorithms;

public class PasswordHasher : IPasswordHasher
{
    private readonly byte[] _key;

    public PasswordHasher(IConfiguration config)
    {
        var key = config["PasswordHasher:SecretKey"] 
                  ?? throw new ArgumentNullException(nameof(config), ErrorMessages.SecretKeyNotFoundError);

        _key = Encoding.UTF8.GetBytes(key);
    }

    public string HashPassword(string password)
    {
        if (password == null)
            throw new ArgumentNullException(nameof(password), ErrorMessages.PasswordError);

        using var hmac = new HMACSHA256(_key);
        var salt = GenerateSalt(16);
        var passwordBytes = Encoding.UTF8.GetBytes(password);

        var saltedPassword = Combine(salt, passwordBytes);
        var hash = hmac.ComputeHash(saltedPassword);
        var result = Combine(salt, hash);

        return Convert.ToBase64String(result);
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        if (hashedPassword == null)
            throw new ArgumentNullException(nameof(hashedPassword), ErrorMessages.HashedPasswordError);
        if (password == null)
            throw new ArgumentNullException(nameof(password), ErrorMessages.PasswordError);

        var hashBytes = Convert.FromBase64String(hashedPassword);
        var salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, salt.Length);

        using var hmac = new HMACSHA256(_key);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = Combine(salt, passwordBytes);
        var computedHash = hmac.ComputeHash(saltedPassword);

        var storedHash = new byte[computedHash.Length];
        Buffer.BlockCopy(hashBytes, salt.Length, storedHash, 0, storedHash.Length);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
    }

    private static byte[] GenerateSalt(int length)
    {
        var salt = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    private static byte[] Combine(byte[] first, byte[] second)
    {
        var result = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, result, 0, first.Length);
        Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
        return result;
    }
}

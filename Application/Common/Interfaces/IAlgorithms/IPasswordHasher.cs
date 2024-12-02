namespace Application.Common.Interfaces.IAlgorithms;

public interface IPasswordHasher
{
    string HashPassword(string password);
    
    bool VerifyHashedPassword(string hashedPassword, string password);
}
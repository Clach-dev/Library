using Domain.Entities;

namespace Domain.Interfaces.IAlgorithms;

public interface ITokensGenerator
{
    public string GenerateAccessToken(User user);

    public RefreshToken GenerateRefreshToken(User user);
}
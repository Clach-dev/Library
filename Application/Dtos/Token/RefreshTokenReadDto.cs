namespace Application.Dtos.Token;

/// <summary>
/// DTO for Refresh Token Read operation
/// for updating access and refresh tokens
/// </summary>
/// <param name="RefreshToken">string that contains refresh token value</param>
public record RefreshTokenReadDto(
    string RefreshToken);
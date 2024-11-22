namespace Application.Dtos.User;

/// <summary>
/// DTO for User Authentication operation
/// </summary>
/// <param name="Login">string which contains User login</param>
/// <param name="Password">string which contains User password</param>
public record UserAuthDto(
    string Login,
    string Password);
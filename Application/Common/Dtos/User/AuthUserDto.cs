namespace Application.Common.Dtos.User;

/// <summary>
/// DTO for User Authentication operation
/// </summary>
/// <param name="PhoneNumber">string which contains User phone number</param>
/// <param name="Password">string which contains User password</param>
public record AuthUserDto(
    string PhoneNumber,
    string Password);
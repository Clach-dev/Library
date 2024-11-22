using Domain.Enums;

namespace Application.Dtos.User;

/// <summary>
/// DTO for User Read operation
/// </summary>
/// <param name="Id">Guid identifier of User</param>
/// <param name="LastName">string which contains Last name of User</param>
/// <param name="FirstName">string which contains First name of User</param>
/// <param name="MiddleName">string which contains Middle name of User</param>
/// <param name="BirthDate">DateTime which contains Birth date of User. Format: dd.mm.yyyy</param>
/// <param name="Role">Enum which represents Role of User. Roles: Admin(0), User(1)</param>
public record UserReadDto(
    Guid Id, 
    string LastName, 
    string FirstName, 
    string? MiddleName, 
    DateTime BirthDate, 
    Roles Role);
using Domain.Enums;

namespace Application.Common.Dtos.User;

/// <summary>
/// Dto for User Role read operation
/// </summary>
/// <param name="UserId">Guid identifier of User</param>
/// <param name="LastName">string which contains Last name of User</param>
/// <param name="FirstName">string which contains First name of User</param>
/// <param name="MiddleName">string which contains Middle name of User</param>
/// <param name="BirthDate">DateTime which contains Birth date of User. Format: dd.mm.yyyy</param>
/// <param name="Role">Enum which represents Role of User</param>
public record ReadUserRoleDto(
    Guid UserId,
    string LastName,
    string FirstName,
    string? MiddleName,
    DateTime BirthDate,
    Roles Role);
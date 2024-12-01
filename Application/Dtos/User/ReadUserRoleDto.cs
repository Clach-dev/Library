using Domain.Enums;

namespace Application.Dtos.User;

public record ReadUserRoleDto(
    Guid UserId,
    string LastName,
    string FirstName,
    string? MiddleName,
    string BirthDate,
    Roles Role);
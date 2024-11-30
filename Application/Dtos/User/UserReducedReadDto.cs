namespace Application.Dtos.User;

/// <summary>
/// Dto for User reduced Read operation
/// </summary>
/// <param name="Id">Guid identifier of User</param>
/// <param name="LastName">string which contains Last name of User</param>
/// <param name="FirstName">string which contains First name of User</param>
public record UserReducedReadDto(
    Guid Id,
    string LastName,
    string FirstName);
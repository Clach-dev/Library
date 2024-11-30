namespace Application.Dtos.Author;

/// <summary>
/// Dto for Author reduced Read operation
/// </summary>
/// <param name="Id">Guid identifier of Author</param>
/// <param name="LastName">string which contains Last name of Author</param>
/// <param name="FirstName">string which contains First name of Author</param>
public record AuthorReducedReadDto(
    Guid Id,
    string LastName,
    string FirstName);
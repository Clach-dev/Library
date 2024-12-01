namespace Application.Dtos.Book;

/// <summary>
/// DTO for Book Read operation
/// </summary>
/// <param name="Id">Guid identifier of Book</param>
/// <param name="ISBN">string which contains ISBN of Book</param>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="Description">string which contains Description of Book</param>
public record ReadBookDto(
    Guid Id,
    string ISBN,
    string Title,
    string? Description);
namespace Application.Dtos.Book;

/// <summary>
/// DTO for Book Update operation
/// </summary>
/// <param name="ISBN">string which contains ISBN of Book</param>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="Description">string which contains Description of Book</param>
public record BookUpdateDto(
    string? ISBN,
    string? Title,
    string? Description);
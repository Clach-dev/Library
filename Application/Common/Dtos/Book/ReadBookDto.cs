namespace Application.Common.Dtos.Book;

/// <summary>
/// DTO for Book Read operation
/// </summary>
/// <param name="Id">Guid identifier of Book</param>
/// <param name="ISBN">string which contains ISBN of Book</param>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="AgeLimit">byte which contains AgeLimit of Book</param>
/// <param name="Description">string which contains Description of Book</param>
/// <param name="Images">IEnumerable of Uri which contains ImageUris of Book</param>
public record ReadBookDto(
    Guid Id,
    string ISBN,
    string Title,
    byte AgeLimit,
    string? Description,
    IEnumerable<Uri>? Images);
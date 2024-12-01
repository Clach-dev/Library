using Application.Dtos.Author;
using Application.Dtos.Genre;

namespace Application.Dtos.Book;

/// <summary>
/// DTO for Book Create operation
/// </summary>
/// <param name="ISBN">string which contains ISBN of Book</param>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="Description">string which contains Description of Book</param>
/// <param name="GenresIds">IEnumerable_Guid which contains Genres IDs of Book</param>
/// <param name="AuthorsIds">IEnumerable_Guid which contains Authors IDs of Book</param>
public record CreateBookDto(
    string ISBN,
    string Title,
    string? Description,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds);
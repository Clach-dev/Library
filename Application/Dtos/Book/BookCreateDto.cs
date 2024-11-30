using Application.Dtos.Author;
using Application.Dtos.Genre;

namespace Application.Dtos.Book;

/// <summary>
/// DTO for Book Create operation
/// </summary>
/// <param name="ISBN">string which contains ISBN of Book</param>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="Description">string which contains Description of Book</param>
/// <param name="Genres">IEnumerable_GenreReducedReadDto which contains Genres of Book</param>
/// <param name="Authors">IEnumerable_AuthorReducedReadDto which contains Authors of Book</param>
public record BookCreateDto(
    string ISBN,
    string Title,
    string? Description,
    IEnumerable<GenreReducedReadDto> Genres,
    IEnumerable<AuthorReducedReadDto> Authors);
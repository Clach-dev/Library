using Application.Dtos.Author;
using Application.Dtos.Genre;

namespace Application.Dtos.Book;

/// <summary>
/// Dto for Book filtering operation
/// </summary>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="Genres">IEnumerable_GenreReducedReadDto which contains Authors of Book</param>
/// <param name="Authors">IEnumerable_AuthorReducedReadDto which contains Genres of Book</param>
public record BookFilterDto(
    string? Title,
    IEnumerable<Guid> Genres,
    IEnumerable<Guid> Authors);

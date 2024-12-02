namespace Application.Common.Dtos.Book;

/// <summary>
/// Dto for Book filtering operation
/// </summary>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="GenresIds">IEnumerable_GenreReducedReadDto which contains Authors of Book</param>
/// <param name="AuthorsIds">IEnumerable_AuthorReducedReadDto which contains Genres of Book</param>
public record FilterBookDto(
    string? Title,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds);

namespace Application.Common.Dtos.Book;

/// <summary>
/// Dto for Book Read operation with pagination.
/// </summary>
/// <param name="Books">IEnumerable_ReadBookDto which contains Books</param>
/// <param name="TotalCount">int which contains TotalCount of books</param>
public record ReadBooksDto(
    IEnumerable<ReadBookDto> Books,
    int TotalCount);
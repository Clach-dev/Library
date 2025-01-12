namespace Application.Common.Dtos.Book;

/// <summary>
/// Dto for Book filtering operation
/// </summary>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="GenresIds"></param>
/// <param name="AuthorsIds"></param>
/// <param name="PageInfo"></param>
public record BooksByFilterDto(
    string? Title,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds,
    PageInfo PageInfo);
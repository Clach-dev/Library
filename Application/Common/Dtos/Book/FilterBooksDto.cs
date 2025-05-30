namespace Application.Common.Dtos.Book;

/// <summary>
/// Dto for Book filtering operation
/// </summary>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="LowerAgeLimit">byte which contains lowerAgeLimit of Book</param>
/// <param name="UpperAgeLimit">byte which contains upperAgeLimit of Book</param>
/// <param name="AuthorsIds">IEnumerable_Guid which contains Authors of Book</param>
/// <param name="GenresIds">IEnumerable_Guid which contains Genres of Book</param>
/// <param name="PageInfoDto"></param>
public record FilterBooksDto(
    string? Title,
    byte? LowerAgeLimit,
    byte? UpperAgeLimit,
    IEnumerable<Guid> AuthorsIds,
    IEnumerable<Guid> GenresIds,
    PageInfoDto PageInfoDto);
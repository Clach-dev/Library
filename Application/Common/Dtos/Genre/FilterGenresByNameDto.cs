namespace Application.Common.Dtos.Genre;

/// <summary>
/// Dto for read genres by name operation
/// </summary>
/// <param name="Name">string that contains name of genre</param>
/// <param name="PageInfoDto">PageInfoDto which contains Pagination information</param>
public record FilterGenresByNameDto(
    string Name,
    PageInfoDto PageInfoDto);
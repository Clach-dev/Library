namespace Application.Common.Dtos.Genre;

/// <summary>
/// Dto for reading a collection of genres with pagination.
/// </summary>
/// <param name="Genres">IEnumerable_ReadGenreDto which contains Genres</param>
/// <param name="TotalCount">int which contains TotalCount of genres</param>
public record ReadGenresDto(
    IEnumerable<ReadGenreDto> Genres,
    int TotalCount);
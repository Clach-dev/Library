namespace Application.Common.Dtos.Genre;

/// <summary>
/// Dto for genres by name read operation
/// </summary>
/// <param name="Name"></param>
/// <param name="PageInfo"></param>
public record GenresByNameDto(
    string Name,
    PageInfo PageInfo);
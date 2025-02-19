namespace Application.Common.Dtos.Genre;

/// <summary>
/// Dto for genres by name read operation
/// </summary>
/// <param name="Name"></param>
/// <param name="PageInfoDto"></param>
public record GenresByNameDto(
    string Name,
    PageInfoDto PageInfoDto);
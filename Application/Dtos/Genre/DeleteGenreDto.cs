namespace Application.Dtos.Genre;

/// <summary>
/// Dto for Genre Delete operation
/// </summary>
/// <param name="Id">Guid identifier of Genre</param>
public record DeleteGenreDto(
    Guid Id);
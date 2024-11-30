namespace Application.Dtos.Genre;

/// <summary>
/// Dto for Genre reduced Read operation
/// </summary>
/// <param name="Id">Guid identifier of Genre</param>
/// <param name="Name">string which contains Name of Genre</param>
public record GenreReducedReadDto(
    Guid Id,
    string Name);
namespace Application.Dtos.Genre;

/// <summary>
/// DTO for Genre Update operation
/// </summary>
/// <param name="Name">string which contains Name of Genre</param>
/// <param name="Description">string which contains Description of Genre</param>
public record GenreUpdateDto(
    string? Name,
    string? Description);
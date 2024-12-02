namespace Application.Common.Dtos.Author;

/// <summary>
/// Dto for Author Delete operation
/// </summary>
/// <param name="Id">Guid identifier of Author</param>
public record DeleteAuthorDto(
    Guid Id);
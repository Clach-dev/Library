namespace Application.Dtos.Book;

/// <summary>
/// Dto for Book Delete operation
/// </summary>
/// <param name="Id">Guid identifier of Book</param>
public record DeleteBookDto(
    Guid Id);
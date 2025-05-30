namespace Application.Common.Dtos.Book;

/// <summary>
/// Dto for Book reduced Read operation
/// </summary>
/// <param name="Id">Guid identifier of Book</param>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="Image">Uri which contains Image Uri of Book</param>
public record ReadBookReducedDto(
    Guid Id,
    string Title,
    Uri? Image);
    // TODO maybe delete if not used
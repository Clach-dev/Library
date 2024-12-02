﻿namespace Application.Dtos.Book;

/// <summary>
/// DTO for Book Update operation
/// </summary>
/// <param name="Id">Guid identifier of Book</param>
/// <param name="ISBN">string which contains ISBN of Book</param>
/// <param name="Title">string which contains Title of Book</param>
/// <param name="Description">string which contains Description of Book</param>
/// <param name="GenresIds">IEnumerable_Guid which contains Genres of Book</param>
/// <param name="AuthorsIds">IEnumerable_Guid which contains Authors of Book</param>
public record UpdateBookDto(
    Guid Id,
    string? ISBN,
    string? Title,
    string? Description,
    IEnumerable<Guid> GenresIds,
    IEnumerable<Guid> AuthorsIds);
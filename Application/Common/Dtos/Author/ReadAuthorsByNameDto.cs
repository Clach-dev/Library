namespace Application.Common.Dtos.Author;

/// <summary>
/// Dto for GetAuthorByName operation
/// </summary>
/// <param name="FirstName">string which contains First name of Author</param>
/// <param name="LastName">string which contains Last name of Author</param>
/// <param name="PageInfo">PageInfo which contains Pagination information</param>
public record ReadAuthorsByNameDto(
    string? FirstName,
    string? LastName,
    PageInfo PageInfo);
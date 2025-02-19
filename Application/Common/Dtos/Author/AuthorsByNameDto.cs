namespace Application.Common.Dtos.Author;

/// <summary>
/// Dto for GetAuthorByName operation
/// </summary>
/// <param name="FirstName">string which contains First name of Author</param>
/// <param name="LastName">string which contains Last name of Author</param>
/// <param name="PageInfoDto">PageInfoDto which contains Pagination information</param>
public record AuthorsByNameDto(
    string? FirstName,
    string? LastName,
    PageInfoDto PageInfoDto);
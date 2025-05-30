namespace Application.Common.Dtos.Author;

/// <summary>
/// Dto for reading authors with pagination.
/// </summary>
/// <param name="Authors">IEnumerable_ReadAuthorDto which contains Authors</param>
/// <param name="TotalCount">int which contains TotalCount of authors</param>
public record ReadAuthorsDto(
    IEnumerable<ReadAuthorDto> Authors,
    int TotalCount);
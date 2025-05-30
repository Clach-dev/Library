namespace Application.Common.Dtos.Review;

/// <summary>
/// Dto for Update Reviews operation
/// </summary>
/// <param name="Id">Guid identifier of Review</param>
/// <param name="Rating">decimal value which contains rating</param>
/// <param name="Comment">string which contains comment</param>
public record UpdateReviewDto(
    Guid Id,
    decimal? Rating,
    string? Comment);
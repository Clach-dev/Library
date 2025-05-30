namespace Application.Common.Dtos.Review;

/// <summary>
/// Dto for Read Reviews operation with pagination
/// </summary>
/// <param name="Reviews">IEnumerable_ReadReviewDto which contains Reviews</param>
/// <param name="TotalCount">int which contains TotalCount of reviews</param>
public record ReadReviewsDto(
    IEnumerable<ReadReviewDto> Reviews,
    int TotalCount);
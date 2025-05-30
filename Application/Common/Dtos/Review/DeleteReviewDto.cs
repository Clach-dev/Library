namespace Application.Common.Dtos.Review;

/// <summary>
/// Dto for Review Delete operation
/// </summary>
/// <param name="Id">Guid identifier of Review</param>
public record DeleteReviewDto(
    Guid Id);
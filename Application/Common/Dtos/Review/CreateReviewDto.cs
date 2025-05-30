namespace Application.Common.Dtos.Review;


/// <summary>
/// DTO for Review Create operation
/// </summary>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="Rating">decimal value which contains rating</param>
/// <param name="Comment">string which contains comment</param>
public record CreateReviewDto(
    Guid BookId,
    decimal Rating,
    string? Comment);
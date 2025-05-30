namespace Application.Common.Dtos.Review;

/// <summary>
/// Dto for Review Delete operation
/// </summary>
/// <param name="Id">Guid identifier of Review</param>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="UserId">Guid identifier of User</param>
/// <param name="Comment">string which contains comment</param>
/// <param name="Rating">decimal value which contains rating</param>
public record ReadReviewDto(
    Guid Id,
    Guid BookId,
    Guid UserId,
    string Comment,
    decimal Rating);
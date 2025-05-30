namespace Application.Common.Dtos.Review;

/// <summary>
/// Dto for Review Delete operation
/// </summary>
/// <param name="UserId">Guid identifier of User</param>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="WithComment">bool which indicates presence of comment in review</param>
/// <param name="PageInfoDto">PageInfoDto which contains page number and page size</param>
public record FilterReviewsDto(
    Guid? UserId,
    Guid? BookId,
    bool? WithComment,
    PageInfoDto PageInfoDto);
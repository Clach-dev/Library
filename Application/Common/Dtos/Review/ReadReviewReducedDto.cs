using Application.Common.Dtos.Book;
using Application.Common.Dtos.User;

namespace Application.Common.Dtos.Review;

/// <summary>
/// Dto for Review Delete operation
/// </summary>
/// <param name="Id">Guid identifier of Review</param>
/// <param name="BookId">Guid identifier of Book</param>
/// <param name="Rating">decimal which represents rating</param>
public record ReadReviewReducedDto(
    Guid Id,
    Guid BookId,
    decimal Rating);
// TODO to delete or to use (if used make validation) 
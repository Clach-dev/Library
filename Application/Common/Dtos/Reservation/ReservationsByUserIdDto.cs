namespace Application.Common.Dtos.Reservation;

/// <summary>
/// Dto for reservations by user id read operation
/// </summary>
/// <param name="UserId">Guid identifier of User</param>
/// <param name="PageInfo">Dto which contains Pagination information</param>
public record ReservationsByUserIdDto(
    Guid UserId,
    PageInfo PageInfo);
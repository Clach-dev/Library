namespace Application.Common.Dtos.Reservation;

public record GetAllReservationsByUserIdDto(
    Guid UserId,
    PageInfo PageInfo);
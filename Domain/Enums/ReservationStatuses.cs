namespace Domain.Enums;

public enum ReservationStatuses : byte
{
    Pending,
    Confirmed,
    Cancelled,
    Completed,
    Expired
}
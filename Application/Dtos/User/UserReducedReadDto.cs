namespace Application.Dtos.User;

public record UserReducedReadDto(
    Guid Id,
    string LastName,
    string FirstName);
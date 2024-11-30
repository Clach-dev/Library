namespace Application.Dtos.Author;

public record AuthorReducedReadDto(
    Guid Id,
    string LastName,
    string FirstName);
namespace Application.Common.Dtos.Genre;

public record GetGenresByNameDto(
    string Name,
    PageInfo PageInfo);
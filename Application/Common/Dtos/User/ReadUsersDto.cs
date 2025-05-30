namespace Application.Common.Dtos.User;

/// <summary>
/// Dto for Read Users operation with pagination
/// </summary>
/// <param name="Users">IEnumerable_ReadUserReducedDto which contains Users</param>
/// <param name="TotalCount">int which contains TotalCount of users</param>
public record ReadUsersDto(
    IEnumerable<ReadUserReducedDto> Users,
    int TotalCount);
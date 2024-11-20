namespace Application.DTOs;

/// <summary>
/// DTO for pagination implementation
/// </summary>
/// <param name="PageNumber">Number of current page</param>
/// <param name="PageSize">Number of items per page</param>
public record PageInfo(int PageNumber = 1, int PageSize = 10);
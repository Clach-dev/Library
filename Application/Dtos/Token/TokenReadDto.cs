﻿namespace Application.Dtos.Token;

/// <summary>
/// DTO for Token Read operation
/// </summary>
/// <param name="AccessToken">string that contains access token value</param>
/// <param name="RefreshToken">string that contains refresh token value</param>
public record TokenReadDto(
    string AccessToken,
    string RefreshToken);
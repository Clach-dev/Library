﻿namespace Application.Dtos.User;

/// <summary>
/// Dto for User Delete operation
/// </summary>
/// <param name="Id">Guid identifier of User</param>
public record UserDeleteDto(
    Guid Id);
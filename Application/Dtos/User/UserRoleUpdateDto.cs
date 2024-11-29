﻿using Domain.Enums;

namespace Application.Dtos.User;

/// <summary>
/// Dto for User Role Update operation
/// </summary>
/// <param name="UserId">Guid identifier of User</param>
/// <param name="Role">Enum which represents Role of User</param>
public record UserRoleUpdateDto(
    Guid UserId,
    Roles Role);

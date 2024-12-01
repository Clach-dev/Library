using Application.Dtos.User;
using Application.Utils;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserRoleCase;

public record UpdateUserRoleCommand(
    Guid UserId,
    Roles Role)
    : IRequest<Result<UpdateUserRoleDto>>, IRequest<Result<ReadUserRoleDto>>;
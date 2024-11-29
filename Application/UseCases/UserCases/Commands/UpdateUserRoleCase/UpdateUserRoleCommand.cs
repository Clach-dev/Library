using Application.Dtos.User;
using Application.Utils;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserRoleCase;

public class UpdateUserRoleCommand : IRequest<Result<UserRoleUpdateDto>>, IRequest<Result<UserRoleReadDto>>
{
    public Guid UserId { get; set; }
    
    public Roles Role { get; set; }
}
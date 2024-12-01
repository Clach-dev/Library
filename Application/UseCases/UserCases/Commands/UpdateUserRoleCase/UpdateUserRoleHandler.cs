using Application.Dtos.User;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserRoleCase;

public class UpdateUserRoleHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateUserRoleCommand, Result<ReadUserRoleDto>>
{
    public async Task<Result<ReadUserRoleDto>> Handle(
        UpdateUserRoleCommand updateUserRoleCommand,
        CancellationToken cancellationToken)
    {   
        var user = await unitOfWork.Users.GetByIdAsync(updateUserRoleCommand.UserId, cancellationToken);

        if (user is null)
        {
            return ResultBuilder.NotFoundResult<ReadUserRoleDto>(ErrorMessages.NotFoundError);
        }

        user.Role = updateUserRoleCommand.Role;
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var userRoleReadDto = mapper.Map<ReadUserRoleDto>(user);
        return ResultBuilder.SuccessResult(userRoleReadDto);
    }
}
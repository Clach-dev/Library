using Application.Dtos.User;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserCase;

public class UpdateUserHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Result<ReadUserDto>>
{
    public async Task<Result<ReadUserDto>> Handle(
        UpdateUserCommand updateUserCommand,
        CancellationToken cancellationToken)
    {
        var currentUser = await unitOfWork.Users.GetByIdAsync(updateUserCommand.Id, cancellationToken);

        if (currentUser is null)
        {
            return ResultBuilder.NotFoundResult<ReadUserDto>(ErrorMessages.NotFoundError);
        }
        
        var existedUser = (await unitOfWork
            .Users
            .GetByPredicateAsync(user => user.Login == updateUserCommand.Login, cancellationToken))
            .FirstOrDefault();
        
        if (existedUser is not null && existedUser.Id != currentUser.Id)
        {
            return ResultBuilder.ConflictResult<ReadUserDto>(ErrorMessages.ExistingUserLoginError);
        }
        
        mapper.Map(updateUserCommand, currentUser);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var userReadDto = mapper.Map<ReadUserDto>(currentUser);
        return ResultBuilder.SuccessResult(userReadDto);
    }
}
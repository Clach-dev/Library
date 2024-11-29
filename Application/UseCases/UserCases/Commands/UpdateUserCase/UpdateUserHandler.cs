using Application.Dtos.User;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.UserCases.Commands.UpdateUserCase;

public class UpdateUserHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<UpdateUserCommand, Result<UserReadDto>>
{
    public async Task<Result<UserReadDto>> Handle(
        UpdateUserCommand updateUserCommand,
        CancellationToken cancellationToken)
    {
        var currentUser = await unitOfWork.Users.GetByIdAsync(updateUserCommand.Id, cancellationToken);

        if (currentUser is null)
        {
            return ResultBuilder.NotFoundResult<UserReadDto>(ErrorMessages.NotFoundError);
        }
        
        mapper.Map(updateUserCommand, currentUser);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var userReadDto = mapper.Map<UserReadDto>(currentUser);
        return ResultBuilder.SuccessResult(userReadDto);
    }
}
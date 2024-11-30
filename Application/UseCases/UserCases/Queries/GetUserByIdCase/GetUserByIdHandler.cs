using Application.Dtos.User;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.UserCases.Queries.GetUserByIdCase;

public class GetUserByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<UserReadDto>>
{
    public async Task<Result<UserReadDto>> Handle(
        GetUserByIdQuery getUserByIdQuery,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(getUserByIdQuery.Id, cancellationToken);
        
        if (user is null)
        {
            ResultBuilder.NotFoundResult<UserReadDto>(ErrorMessages.UserIdNotFound);
        }
        
        var userReadDto = mapper.Map<UserReadDto>(user);

        return ResultBuilder.SuccessResult(userReadDto);
    }
}
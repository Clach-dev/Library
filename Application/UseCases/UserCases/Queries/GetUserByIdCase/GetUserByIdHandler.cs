using Application.Common.Dtos.User;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.UserCases.Queries.GetUserByIdCase;

public class GetUserByIdHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetUserByIdQuery, Result<ReadUserDto>>
{
    public async Task<Result<ReadUserDto>> Handle(
        GetUserByIdQuery getUserByIdQuery,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByIdAsync(getUserByIdQuery.Id, cancellationToken);
        if (user is null)
        {
            ResultBuilder.NotFoundResult<ReadUserDto>(ErrorMessages.UserIdNotFound);
        }
        
        var userReadDto = mapper.Map<ReadUserDto>(user);

        return ResultBuilder.SuccessResult(userReadDto);
    }
}
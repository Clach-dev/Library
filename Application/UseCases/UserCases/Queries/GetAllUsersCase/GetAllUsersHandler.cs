using Application.Common.Dtos.User;
using Application.Common.Interfaces.IRepositories;
using Application.Common.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.UserCases.Queries.GetAllUsersCase;

public class GetAllUsersHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<ReadUserDto>>>
{
    public async Task<Result<IEnumerable<ReadUserDto>>> Handle(
        GetAllUsersQuery getAllUsersQuery,
        CancellationToken cancellationToken)
    {
        var users = await unitOfWork.Users.GetAllAsync(getAllUsersQuery, cancellationToken);

        var usersReadDto = mapper.Map<IEnumerable<ReadUserDto>>(users);

        return ResultBuilder.SuccessResult(usersReadDto);
    }
}
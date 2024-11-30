using Application.Dtos.User;
using Application.Interfaces.IRepositories;
using Application.Utils;
using AutoMapper;
using MediatR;

namespace Application.UseCases.UserCases.Queries.GetAllUsersCase;

public class GetAllUsersHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserReadDto>>>
{
    public async Task<Result<IEnumerable<UserReadDto>>> Handle(
        GetAllUsersQuery getAllUsersQuery,
        CancellationToken cancellationToken)
    {
        var users = await unitOfWork.Users.GetAllAsync(getAllUsersQuery, cancellationToken);

        var usersReadDto = mapper.Map<IEnumerable<UserReadDto>>(users);

        return ResultBuilder.SuccessResult(usersReadDto);
    }
}
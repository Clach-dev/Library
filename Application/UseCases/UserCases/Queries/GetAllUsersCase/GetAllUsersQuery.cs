using Application.Dtos;
using Application.Dtos.User;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Queries.GetAllUsersCase;

public record GetAllUsersQuery
    : PageInfo, IRequest<Result<IEnumerable<UserReadDto>>>;
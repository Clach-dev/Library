using Application.Common.Dtos;
using Application.Common.Dtos.User;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Queries.GetAllUsersCase;

public record GetAllUsersQuery(
    PageInfo PageInfo)
    : IRequest<Result<IEnumerable<ReadUserDto>>>;

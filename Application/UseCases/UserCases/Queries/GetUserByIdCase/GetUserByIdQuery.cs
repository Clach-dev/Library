using Application.Dtos.User;
using Application.Utils;
using MediatR;

namespace Application.UseCases.UserCases.Queries.GetUserByIdCase;

public record GetUserByIdQuery(Guid Id) : IRequest<Result<UserReadDto>>;
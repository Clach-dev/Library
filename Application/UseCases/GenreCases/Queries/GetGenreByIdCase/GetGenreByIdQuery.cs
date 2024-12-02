using Application.Common.Dtos.Genre;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.GenreCases.Queries.GetGenreByIdCase;

public record GetGenreByIdQuery(
    Guid Id)
    : IRequest<Result<ReadGenreDto>>;
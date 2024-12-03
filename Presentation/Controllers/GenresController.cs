using Application.Common.Dtos;
using Application.Common.Dtos.Genre;
using Application.UseCases.GenreCases.Commands.CreateGenreCase;
using Application.UseCases.GenreCases.Commands.DeleteGenreCase;
using Application.UseCases.GenreCases.Commands.UpdateGenreCase;
using Application.UseCases.GenreCases.Queries.GetAllGenresCase;
using Application.UseCases.GenreCases.Queries.GetGenreByIdCase;
using Application.UseCases.GenreCases.Queries.GetGenresByNameCase;
using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Policy = Policies.OnlyAdminAccess)]
public class GenresController(
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IMediator mediator)
    : CustomControllerBase(httpContextAccessor)
{
    /// <summary>
    /// Genre create operation
    /// </summary>
    /// <param name="createGenreDto">CreateGenreDto which contains genre information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of genre creation</returns>
    [HttpPost]
    public async Task<IActionResult> CreateGenre(
        [FromBody] CreateGenreDto createGenreDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<CreateGenreCommand>(createGenreDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Genre update operation
    /// </summary>
    /// <param name="updateGenreDto">UpdateGenreDto which contains genre update information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of genre update operation</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateGenre(
        [FromBody] UpdateGenreDto updateGenreDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<UpdateGenreCommand>(updateGenreDto), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Genre delete operation
    /// </summary>
    /// <param name="createGenreDto">CreateGenreDto which contains id of genre to delete</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of genre delete operation</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteGenre(
        [FromBody] DeleteGenreDto createGenreDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<DeleteGenreCommand>(createGenreDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Get all genres operation
    /// </summary>
    /// <param name="pageInfo">PageInfo which contains number of current page and number of items per page</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of Getting all genres operation</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllGenres(
        [FromQuery]PageInfo pageInfo,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllGenresQuery(pageInfo), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Get genre by id operation
    /// </summary>
    /// <param name="genreId">Guid identifier of genre</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of getting genre by id operation</returns>
    [HttpGet("{genreId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenreById(
        Guid genreId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetGenreByIdQuery(genreId), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Get genres by name operation
    /// </summary>
    /// <param name="getGenresByNameQuery">GetGenresByNameQuery which contains genre  name</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result of getting genres by name operation</returns>
    [HttpGet("name")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGenresByName(
        [FromBody] GetGenresByNameQuery getGenresByNameQuery,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(getGenresByNameQuery, cancellationToken);
        return Result(result);
    }
}
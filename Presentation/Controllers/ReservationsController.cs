using Application.Common.Dtos;
using Application.Common.Dtos.Reservation;
using Application.UseCases.ReservationCases.Commands.CreateReservationCase;
using Application.UseCases.ReservationCases.Commands.DeleteReservationCase;
using Application.UseCases.ReservationCases.Commands.UpdateReservationCase;
using Application.UseCases.ReservationCases.Queries.GetAllReservationsByUserIdCase;
using Application.UseCases.ReservationCases.Queries.GetAllReservationsCase;
using Application.UseCases.ReservationCases.Queries.GetReservationByIdCase;
using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ReservationsController(
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IMediator mediator)
    : CustomControllerBase(httpContextAccessor)
{
    /// <summary>
    /// Get all reservations operation
    /// </summary>
    /// <param name="pageInfoDto">PageInfoDto which contains number of current page and number of items per page</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with reservations information</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllReservations(
        [FromQuery] PageInfoDto pageInfoDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllReservationsQuery(pageInfoDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Get reservation by id operation
    /// </summary>
    /// <param name="reservationId">Guid identifier of reservation</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with reservation information</returns>
    [HttpGet("{reservationId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetReservationById(
        [FromRoute] Guid reservationId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetReservationByIdQuery(reservationId), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Get reservations by user id operation
    /// </summary>
    /// <param name="reservationsByUserIdDto">ReservationsByUserIdDto which contains user id</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result with user reservations information</returns>
    [HttpGet("user/{userId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetReservationsByUserId(
        [FromQuery] ReservationsByUserIdDto reservationsByUserIdDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<GetAllReservationsByUserIdQuery>(reservationsByUserIdDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Reservation create operation
    /// </summary>
    /// <param name="createReservationDto">CreateReservationDto which contains reservation information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with created reservation information</returns>
    [HttpPost]
    [Authorize(Policy = Policies.AuthenticateAccess)]
    public async Task<IActionResult> CreateReservation(
        [FromBody] CreateReservationDto createReservationDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<CreateReservationCommand>(createReservationDto), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Reservation update operation
    /// </summary>
    /// <param name="updateReservationDto">UpdateReservationDto which contains reservation update information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with updated reservation information</returns>
    [HttpPut]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> UpdateReservation(
        [FromBody] UpdateReservationDto updateReservationDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<UpdateReservationCommand>(updateReservationDto), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Reservation delete operation
    /// </summary>
    /// <param name="deleteReservationDto">DeleteReservationDto which contains id of reservation to delete</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with status code of delete operation</returns>
    [HttpDelete]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> DeleteReservation(
        [FromBody] DeleteReservationDto deleteReservationDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<DeleteReservationCommand>(deleteReservationDto), cancellationToken);
        return Result(result);
    }
}
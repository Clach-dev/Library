using Application.Common.Dtos;
using Application.Common.Dtos.Review;
using Application.UseCases.ReviewCases.Commands.CreateReviewCase;
using Application.UseCases.ReviewCases.Commands.DeleteReviewCase;
using Application.UseCases.ReviewCases.Commands.UpdateReviewCase;
using Application.UseCases.ReviewCases.Queries.GetAllReviewsCase;
using Application.UseCases.ReviewCases.Queries.GetReviewByIdCase;
using Application.UseCases.ReviewCases.Queries.GetReviewsByFilterCase;
using AutoMapper;
using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ReviewsController(
    IHttpContextAccessor httpContextAccessor,
    IMapper mapper,
    IMediator mediator)
    : CustomControllerBase(httpContextAccessor)
{
    /// <summary>
    /// Get all reviews operation
    /// </summary>
    /// <param name="pageInfoDto">PageInfoDto which contains number of current page and number of items per page</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with reviews information</returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllReviews(
        [FromQuery] PageInfoDto pageInfoDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetAllReviewsQuery(pageInfoDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Get review by id operation
    /// </summary>
    /// <param name="reviewId">Guid identifier of review</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with review information</returns>
    [HttpGet("{reviewId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetReviewById(
        [FromRoute] Guid reviewId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetReviewByIdQuery(reviewId), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Get reviews by filter operation
    /// </summary>
    /// <param name="filterReviewsDto">FilterReviewsDto which contains filtering information</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result with reviews information</returns>
    [HttpGet("filter")]
    [AllowAnonymous]
    public async Task<IActionResult> GetReviewsByFilter(
        [FromQuery] FilterReviewsDto filterReviewsDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<GetReviewsByFilterQuery>(filterReviewsDto), cancellationToken);
        return Result(result);
    }
    
    /// <summary>
    /// Review create operation
    /// </summary>
    /// <param name="createReviewDto">CreateReviewDto which contains review information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with created review information</returns>
    [HttpPost]
    [Authorize(Policy = Policies.AuthenticateAccess)]
    public async Task<IActionResult> CreateReview(
        [FromBody] CreateReviewDto createReviewDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<CreateReviewCommand>(createReviewDto), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Review update operation
    /// </summary>
    /// <param name="updateReviewDto">UpdateReviewDto which contains review update information</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with updated review information</returns>
    [HttpPut]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> UpdateReview(
        [FromBody] UpdateReviewDto updateReviewDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<UpdateReviewCommand>(updateReviewDto), cancellationToken);
        return Result(result);
    }

    /// <summary>
    /// Review delete operation
    /// </summary>
    /// <param name="deleteReviewDto">DeleteReviewDto which contains id of review to delete</param>
    /// <param name="cancellationToken">CancellationToken token of operation cancel</param>
    /// <returns>Result with status code of delete operation</returns>
    [HttpDelete]
    [Authorize(Policy = Policies.OnlyAdminAccess)]
    public async Task<IActionResult> DeleteReview(
        [FromBody] DeleteReviewDto deleteReviewDto,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Send(mapper.Map<DeleteReviewCommand>(deleteReviewDto), cancellationToken);
        return Result(result);
    }
}
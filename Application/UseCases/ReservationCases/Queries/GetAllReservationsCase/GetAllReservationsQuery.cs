﻿using Application.Common.Dtos;
using Application.Common.Dtos.Reservation;
using Application.Common.Utils;
using MediatR;

namespace Application.UseCases.ReservationCases.Queries.GetAllReservationsCase;

public record GetAllReservationsQuery(
    PageInfoDto PageInfoDto)
    : IRequest<Result<IEnumerable<ReadReservationDto>>>;
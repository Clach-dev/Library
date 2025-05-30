using Application.Common.Dtos.Reservation;
using FluentValidation;

namespace Presentation.Common.Validators.Reservation;

public class DeleteReservationDtoValidator : AbstractValidator<DeleteReservationDto>
{
    public DeleteReservationDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();
    }   
}
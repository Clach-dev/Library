using Application.Common.Dtos.Reservation;
using FluentValidation;

namespace Presentation.Common.Validators.Reservation;

public class CreateReservationDtoValidator : AbstractValidator<CreateReservationDto>
{
    public CreateReservationDtoValidator()
    {
        RuleFor(x => x.BookId)
            .GuidRule();
    }
}
using Application.Common.Dtos.Reservation;
using FluentValidation;

namespace Presentation.Common.Validators.Reservation;

public class UpdateReservationDtoValidator : AbstractValidator<UpdateReservationDto>
{
    public UpdateReservationDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();
        
        RuleFor(x => x.BookId)
            .GuidRule();
        
        RuleFor(x => x.UserId)
            .GuidRule();
        
        RuleFor(x => x.ReceiptDate)
            .ReceiptDateRule();
        
        RuleFor(x => x.ReturnDate)
            .ReturnDateRule();
        
        RuleFor(x => x.Status)
            .ReservationStatusRule();
    }
}
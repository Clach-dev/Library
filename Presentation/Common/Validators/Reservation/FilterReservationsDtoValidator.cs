using Application.Common.Dtos.Reservation;
using FluentValidation;

namespace Presentation.Common.Validators.Reservation;

public class FilterReservationsDtoValidator : AbstractValidator<FilterReservationsDto>
{
    public FilterReservationsDtoValidator()
    {
        RuleFor(x => x.UserId)
            .GuidRule();
        
        RuleFor(x => x.BookId)
            .GuidRule();
        
        RuleFor(x => x.FromRecieptDate)
            .ReceiptDateRule();
        
        RuleFor(x => x.ToRecieptDate)
            .ReceiptDateRule();
        
        RuleFor(x => x.FromReturnDate)
            .ReturnDateRule();
        
        RuleFor(x => x.ToReturnDate)
            .ReturnDateRule();
        
        RuleFor(x => x.Status)
            .ReservationStatusRule();
        
        RuleFor(x => x.PageInfoDto)
            .SetValidator(new PageInfoValidator());
    }
}
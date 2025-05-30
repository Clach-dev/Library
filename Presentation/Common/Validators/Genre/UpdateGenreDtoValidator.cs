using Application.Common.Dtos.Genre;
using FluentValidation;

namespace Presentation.Common.Validators.Genre;

public class UpdateGenreDtoValidator : AbstractValidator<UpdateGenreDto>
{
    public UpdateGenreDtoValidator()
    {
        RuleFor(x => x.Id)
            .GuidRule();
        
        RuleFor(x => x.Name)
            .TitleRule();
        
        RuleFor(x => x.Description)
            .DescriptionRule();
    }
}
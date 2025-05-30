using Application.Common.Dtos.Genre;
using FluentValidation;

namespace Presentation.Common.Validators.Genre;

public class CreateGenreDtoValidator : AbstractValidator<CreateGenreDto>
{
    public CreateGenreDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNullRule()
            .TitleRule();

        RuleFor(x => x.Description)
            .DescriptionRule();
    }
}
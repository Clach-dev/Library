using Application.Common.Dtos;
using FluentValidation;

namespace Presentation.Common.Validators;

public class PageInfoValidator : AbstractValidator<PageInfoDto>
{
    public PageInfoValidator()
    {
        RuleFor(x => x.PageNumber)
            .PageSettingsRule();

        RuleFor(x => x.PageSize)
            .PageSettingsRule();
    }
}
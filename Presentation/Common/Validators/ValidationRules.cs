using FluentValidation;

namespace Presentation.Common.Validators;

public static class ValidationRules
{
    private const string NameRegex = "^[a-zA-Z'-]+$";
    
    public static IRuleBuilder<T, Guid> GuidRule<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEqual(Guid.Empty);
    }
    
    public static IRuleBuilder<T, int> PageSettingsRule<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .NotNull().WithMessage("Page settings cannot be null.")
            .Must(x => x > 0).WithMessage("Page number must be greater than 0.");
    }
    
    public static IRuleBuilder<T, string> LastNameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(50).WithMessage("The last name must not exceed 20 characters.")
            .Matches(NameRegex).WithMessage("The last name can only contain letters, apostrophes, and hyphens.");
    }
    
    public static IRuleBuilder<T, string> FirstNameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(50).WithMessage("The first name must not exceed 20 characters.")
            .Matches(NameRegex).WithMessage("The first name can only contain letters, apostrophes, and hyphens.");
    }
    
    public static IRuleBuilder<T, string> MiddleNameRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .MaximumLength(50).WithMessage("The middle name must not exceed 20 characters.")
            .Matches(NameRegex).WithMessage("The middle name can only contain letters, apostrophes, and hyphens.");
    }

    public static IRuleBuilder<T, string> DescriptionRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.
            MaximumLength(300).WithMessage("The description must not exceed 300 characters.");
    }
    
    public static IRuleBuilder<T, DateTime> DateOfBirthRule<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("The date of birth is required.")
            .LessThan(DateTime.Now).WithMessage("The date of birth must be in the past.");
    }
}
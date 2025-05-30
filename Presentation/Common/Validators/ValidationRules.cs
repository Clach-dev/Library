using Domain.Enums;
using FluentValidation;

namespace Presentation.Common.Validators;

public static class ValidationRules
{
    private const string NameRegex = "^[a-zA-Z'-]+$";
    
    private const string PhoneRegex = @"^\+?[0-9\s\-\(\)]{7,20}$";
    
    private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];
    
    private const long MaxImageSizeInBytes = 5 * 1024 * 1024;
    
    public static IRuleBuilderOptions<T, TProperty> NotNullRule<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder
            .NotNull().WithMessage($"The {typeof(TProperty).Name} can't be null.");
    }

    private static IRuleBuilderOptions<T, TProperty> NotEmptyRule<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"The {typeof(TProperty).Name} is required.");
    }
    
    public static IRuleBuilderOptions<T, Guid> GuidRule<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEqual(Guid.Empty);
    }
    
    public static IRuleBuilderOptions<T, Guid?> GuidRule<T>(this IRuleBuilder<T, Guid?> ruleBuilder)
    {
        return ruleBuilder
            .NotEqual(Guid.Empty)
            .When(x => x is not null);
    }
    
    public static IRuleBuilderOptions<T, int> PageSettingsRule<T>(this IRuleBuilder<T, int> ruleBuilder)
    {
        return ruleBuilder
            .NotNullRule()
            .Must(x => x > 0).WithMessage("Page number must be greater than 0.");
    }
    
    private static IRuleBuilderOptions<T, string?> NameRuleBuilder<T>(this IRuleBuilder<T, string?> ruleBuilder, string type)
    {
        return ruleBuilder
            .NotEmptyRule()
            .MaximumLength(50).WithMessage("The {PropertyName} name must not exceed 50 characters.")
            .Matches(NameRegex).WithMessage($"The {type} name can only contain letters, apostrophes, and hyphens.")
            .When(x => x is not null);
    }
    
    public static IRuleBuilderOptions<T, string?> LastNameRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NameRuleBuilder("last");
    }
    
    public static IRuleBuilderOptions<T, string?> FirstNameRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NameRuleBuilder("first");
    }
    
    public static IRuleBuilderOptions<T, string?> MiddleNameRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NameRuleBuilder("middle");
    }

    
    public static IRuleBuilderOptions<T, string?> DescriptionRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmptyRule()
            .MaximumLength(300).WithMessage("The description must not exceed 300 characters.")
            .When(x => x is not null);
    }
    
    public static IRuleBuilderOptions<T, string?> ISBNRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmptyRule()
            .MaximumLength(20).WithMessage("The ISBN must not exceed 20 characters.")
            .When(x => x is not null);
    }

    public static IRuleBuilderOptions<T, string?> TitleRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmptyRule()
            .MaximumLength(150).WithMessage("The title must not exceed 150 characters.")
            .When(x => x is not null);
    }

    public static IRuleBuilderOptions<T, byte> AgeLimitRule<T>(this IRuleBuilder<T, byte> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThanOrEqualTo((byte)0).WithMessage("The age limit must be greater than 0.")
            .LessThanOrEqualTo((byte)25).WithMessage("The age limit must be less than 25.");
    }
    
    public static IRuleBuilderOptions<T, byte?> AgeLimitRule<T>(this IRuleBuilder<T, byte?> ruleBuilder)
    {
        return ruleBuilder
            .GreaterThanOrEqualTo((byte)0).WithMessage("The age limit must be greater than 0.")
            .LessThanOrEqualTo((byte)25).WithMessage("The age limit must be less than 25.")
            .When(x => x is not null);
    }

    public static IRuleBuilderOptions<T, IEnumerable<IFormFile>> ImagesRule<T>(this IRuleBuilder<T, IEnumerable<IFormFile>> ruleBuilder)
    {
        return ruleBuilder
            .Must(list => list == null || list.Count() <= 10)
            .WithMessage("You can upload up to 10 images.")
            .Must(list => list == null || list.All(f => f.Length > 0))
            .WithMessage("All uploaded images must not be empty.");
    }
    
    public static IRuleBuilderOptions<T, IEnumerable<Guid>> GuidListRule<T>(this IRuleBuilder<T, IEnumerable<Guid>> ruleBuilder)
    {
        return ruleBuilder
            .Must(list => list == null || list.Count() <= 6)
            .WithMessage("List can't contain more than 6 GUIDs.")
            .Must(list => list == null || list.All(g => g != Guid.Empty))
            .WithMessage("List must not contain empty GUIDs.");
    }
    
    public static IRuleBuilderOptions<T, IEnumerable<Uri>> UrisRule<T>(this IRuleBuilder<T, IEnumerable<Uri>> ruleBuilder)
    {
        return ruleBuilder
            .Must(list => list == null || list.Count() <= 10)
            .WithMessage("You can keep up to 10 images.")
            .Must(list => list == null || list.All(uri => uri is { IsAbsoluteUri: true }))
            .WithMessage("All image URIs must be valid absolute URIs.");
    }
    
    public static IRuleBuilderOptions<T, DateTime?> ReceiptDateRule<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date is null || (date.Value <= DateTime.Now && date.Value >= DateTime.Now.AddYears(-10)))
            .WithMessage("The date must be within the past 10 years and not in the future.");
    }
    
    public static IRuleBuilderOptions<T, DateTime?> ReturnDateRule<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date is null || (date.Value <= DateTime.Now.AddYears(-10) && date.Value >= DateTime.Now.AddYears(1)))
            .WithMessage("The date must be within the past 10 years and not more then 10 years in the future.");
    }

    public static IRuleBuilderOptions<T, ReservationStatuses?> ReservationStatusRule<T>(
        this IRuleBuilder<T, ReservationStatuses?> ruleBuilder)
    {
        return ruleBuilder
            .Must(status => status == null || Enum.IsDefined(typeof(ReservationStatuses), status))
            .WithMessage("The reservation status is invalid.");
    }
    
    public static IRuleBuilderOptions<T, decimal> RatingRule<T>(this IRuleBuilder<T, decimal> ruleBuilder)
    {
        return ruleBuilder
            .InclusiveBetween(0, 10)
            .WithMessage("The rating must be between 0 and 10.");
    }
    
    public static IRuleBuilderOptions<T, decimal?> RatingRule<T>(this IRuleBuilder<T, decimal?> ruleBuilder)
    {
        return ruleBuilder
            .InclusiveBetween(0, 10)
            .When(x => x is not null)
            .WithMessage("The rating must be between 0 and 10.");
    }
    
    public static IRuleBuilderOptions<T, bool?> WithCommentRule<T>(this IRuleBuilder<T, bool?> ruleBuilder)
    {
        return ruleBuilder
            .Must(value => value is true or false)
            .When(value => value is not null)
            .WithMessage("The WithComment field must be true, false, or null.");
    }
    
    public static IRuleBuilderOptions<T, string?> PhoneNumberRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("The phone number is required.")
            .Matches(PhoneRegex).WithMessage("Invalid phone number format.")
            .When(x => x is not null);
    }

    public static IRuleBuilderOptions<T, string?> PasswordRule<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("The password is required.")
            .MinimumLength(8).WithMessage("The password must be at least 8 characters long.")
            .Matches(@"[A-Za-z]").WithMessage("The password must contain at least one letter.")
            .Matches(@"\d").WithMessage("The password must contain at least one number.")
            .When(x => x is not null);
    }
    
    public static IRuleBuilderOptions<T, Roles> RoleRule<T>(this IRuleBuilder<T, Roles> ruleBuilder)
    {
        return ruleBuilder
            .IsInEnum().WithMessage("Invalid role value. Allowed roles: Admin, User.");
    }
    
    public static IRuleBuilderOptions<T, DateTime> BirthDateRule<T>(this IRuleBuilder<T, DateTime> ruleBuilder)
    {
        return ruleBuilder
            .LessThan(DateTime.Now).WithMessage("The date must be in the past.")
            .GreaterThan(DateTime.Now.AddYears(-150)).WithMessage("The date can't be more than 150 years ago.");
    }
    
    public static IRuleBuilderOptions<T, DateTime?> BirthDateRule<T>(this IRuleBuilder<T, DateTime?> ruleBuilder)
    {
        return ruleBuilder
            .Must(date => date is null || (date.Value <= DateTime.Now && date.Value >= DateTime.Now.AddYears(-150)))
            .WithMessage("The date must be within the past 150 years and not in the future.");
    }
    
    public static IRuleBuilderOptions<T, IFormFile?> ProfileImageRule<T>(this IRuleBuilder<T, IFormFile?> ruleBuilder)
    {
        return ruleBuilder
            .Must(file => file == null || file.Length > 0)
            .WithMessage("The profile image must not be empty.")
            .Must(file => file == null || file.Length <= MaxImageSizeInBytes)
            .WithMessage("The profile image must not exceed 5 MB.")
            .Must(file => file == null || AllowedImageExtensions.Contains(Path.GetExtension(file.FileName).ToLower()))
            .WithMessage("The profile image must be a .jpg, .jpeg, .png, or .webp file.");
    }
}
using FluentValidation;
using FluentValidation.Results;

namespace Sample.MongoDb.Api.Domain.ValueObjects;

public class Assessment : AbstractValidator<Assessment>
{
    public Assessment(int stars, string comment)
    {
        Stars = stars;
        Comment = comment;
    }

    public int Stars { get; private set; }

    public string Comment { get; private set; }

    public FluentValidation.Results.ValidationResult ValidationResult { get; set; }

    public virtual bool Validate()
    {
        ValidateStars();
        ValidateComment();

        ValidationResult = Validate(this);

        return ValidationResult.IsValid;
    }

    private void ValidateStars()
    {
        RuleFor(c => c.Stars)
            .GreaterThan(0).WithMessage("Number of stars must be greater than zero.")
            .LessThanOrEqualTo(5).WithMessage("Number of stars must be less than or equal to five.");
    }

    private void ValidateComment()
    {
        RuleFor(c => c.Comment)
            .NotEmpty().WithMessage("Comment cannot be empty.")
            .MaximumLength(100).WithMessage("Comment can have a maximum of 100 characters.");
    }
}

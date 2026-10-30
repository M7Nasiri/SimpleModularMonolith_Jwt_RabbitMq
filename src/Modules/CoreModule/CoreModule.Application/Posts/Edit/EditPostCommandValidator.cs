using Common.Application.Validation;
using FluentValidation;

namespace Application.Posts.Edit;

public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
{
    public EditPostCommandValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Text)
            .NotEmpty().WithMessage(ValidationMessages.required("متن"));

        RuleFor(r => r.Id)
            .NotEmpty().WithMessage(ValidationMessages.required("شناسه پست"));
    }
}
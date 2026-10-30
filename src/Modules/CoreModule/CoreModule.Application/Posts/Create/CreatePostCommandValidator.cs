using Common.Application.Validation;
using FluentValidation;

namespace Application.Posts.Create;

public class CreatePostCommandHandlerValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandHandlerValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty().WithMessage(ValidationMessages.required("عنوان"));

        RuleFor(r => r.Text)
            .NotEmpty().WithMessage(ValidationMessages.required("متن"));

        RuleFor(r => r.UserId)
            .NotEmpty().WithMessage(ValidationMessages.required("نویسنده"));
    }
}
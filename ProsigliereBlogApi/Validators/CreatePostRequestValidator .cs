using FluentValidation;
using ProsigliereBlogApi.DTOs.Requests;

namespace ProsigliereBlogApi.Validators
{
    public class CreatePostRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreatePostRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200);

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required")
                .MinimumLength(10);
        }
    }
}

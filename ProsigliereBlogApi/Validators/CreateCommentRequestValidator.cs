using FluentValidation;
using ProsigliereBlogApi.DTOs.Requests;

namespace ProsigliereBlogApi.Validators
{
    public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
    {
        public CreateCommentRequestValidator()
        {
            RuleFor(x => x.Author)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Content)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(500);
        }
    }
}

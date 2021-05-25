using FluentValidation;
using Voter.Dto.Requests.Variant;

namespace Voter.Core.Validators
{
    public class AddVariantRequestValidator : AbstractValidator<AddVariantRequest>
    {
        public AddVariantRequestValidator()
        {
            RuleFor(x => x.QuestionId)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));

            RuleFor(x => x.Text)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidVariantText(x));
        }
    }
}

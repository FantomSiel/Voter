using FluentValidation;
using Voter.Dto.Requests.Variant;

namespace Voter.Core.Validators
{
    public class UpdateVariantRequestValidator : AbstractValidator<UpdateVariantRequest>
    {
        public UpdateVariantRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));

            RuleFor(x => x.Text)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidVariantText(x));
        }
    }
}

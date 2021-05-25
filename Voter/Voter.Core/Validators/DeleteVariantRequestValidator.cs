using FluentValidation;
using Voter.Dto.Requests.Variant;

namespace Voter.Core.Validators
{
    public class DeleteVariantRequestValidator : AbstractValidator<DeleteVariantRequest>
    {
        public DeleteVariantRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));
        }
    }
}

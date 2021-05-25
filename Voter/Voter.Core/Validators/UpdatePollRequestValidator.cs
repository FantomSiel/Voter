using FluentValidation;
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Validators
{
    public class UpdatePollRequestValidator : AbstractValidator<UpdatePollRequest>
    {
        public UpdatePollRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));

            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidName(x));

            RuleFor(x => x.Description)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidDescription(x));
        }
    }
}

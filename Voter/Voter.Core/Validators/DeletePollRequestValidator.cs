using FluentValidation;
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Validators
{
    public class DeletePollRequestValidator : AbstractValidator<DeletePollRequest>
    {
        public DeletePollRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));
        }
    }
}

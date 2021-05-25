using FluentValidation;
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Validators
{
    public class GetPollRequestValidator : AbstractValidator<GetPollRequest>
    {
        public GetPollRequestValidator()
        {

            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));
        }
    }
}

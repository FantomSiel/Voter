using FluentValidation;
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Validators
{
    public class GetStatsRequestValidator : AbstractValidator<GetStatsRequest>
    {
        public GetStatsRequestValidator()
        {

            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));
        }
    }
}

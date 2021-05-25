using FluentValidation;
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Validators
{
    public class GetPollListRequestValidator : AbstractValidator<GetPollListRequest>
    {
        public GetPollListRequestValidator()
        {
            RuleFor(x => x.Limit)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Offset)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.UserId)
                .Must(x => UtilityValidationMethods.IsGuid(x))
                .When(x => !string.IsNullOrEmpty(x.UserId));
        }
    }
}

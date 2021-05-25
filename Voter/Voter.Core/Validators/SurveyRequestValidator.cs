using FluentValidation;
using System.Linq;
using Voter.Dto.Requests.Poll;

namespace Voter.Core.Validators
{
    public class SurveyRequestValidator : AbstractValidator<SurveyRequest>
    {
        public SurveyRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));

            RuleFor(x => x.Responses)
                .Must(x => x.All(i =>
                    UtilityValidationMethods.IsGuid(i.QuestionId) &&
                    i.Variants.All(v => UtilityValidationMethods.IsGuid(v))));
        }
    }
}

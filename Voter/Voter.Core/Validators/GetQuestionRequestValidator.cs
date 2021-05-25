using FluentValidation;
using Voter.Dto.Requests.Question;

namespace Voter.Core.Validators
{
    public class GetQuestionRequestValidator : AbstractValidator<GetQuestionRequest>
    {
        public GetQuestionRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));
        }
    }
}

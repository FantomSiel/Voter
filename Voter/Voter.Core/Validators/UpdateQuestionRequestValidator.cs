using FluentValidation;
using Voter.Dto.Requests.Question;

namespace Voter.Core.Validators
{
    public class UpdateQuestionRequestValidator : AbstractValidator<UpdateQuestionRequest>
    {
        public UpdateQuestionRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));

            RuleFor(x => x.Text)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidQuestionText(x));
        }
    }
}

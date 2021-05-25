using FluentValidation;
using Voter.Dto.Requests.Question;

namespace Voter.Core.Validators
{
    public class DeleteQuestionRequestValidator : AbstractValidator<DeleteQuestionRequest>
    {
        public DeleteQuestionRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));
        }
    }
}

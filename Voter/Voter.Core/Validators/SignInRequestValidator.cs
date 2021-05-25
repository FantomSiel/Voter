using FluentValidation;
using Voter.Dto.Requests.User;

namespace Voter.Core.Validators
{
    public class SignInRequestValidator : AbstractValidator<SignInRequest>
    {
        public SignInRequestValidator()
        {
            RuleFor(x => x.Header)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsBasicAuthHeader(x));
        }
    }
}

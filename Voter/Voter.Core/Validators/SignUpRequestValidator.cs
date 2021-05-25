using FluentValidation;
using Voter.Dto.Requests.User;

namespace Voter.Core.Validators
{
    public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
    {
        public SignUpRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidUserName(x));

            RuleFor(x => x.Login)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidMail(x));

            RuleFor(x => x.Country)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidCountry(x));

            RuleFor(x => x.Sex)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidSex(x));

            RuleFor(x => x.Sex)
                .NotNull().NotEmpty()
                .MaximumLength(15)
                .MinimumLength(4);

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(10)
                .LessThanOrEqualTo(100);

        }
    }
}

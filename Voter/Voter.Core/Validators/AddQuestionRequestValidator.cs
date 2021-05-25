using AutoMapper;
using FluentValidation;
using System.Linq;
using Voter.Dto.Requests.Question;
using Voter.Dto.Requests.Variant;

namespace Voter.Core.Validators
{
    public class AddQuestionRequestValidator : AbstractValidator<AddQuestionRequest>
    {
        private AbstractValidator<AddVariantRequest> _variantValidator;
        private IMapper _mapper;

        public AddQuestionRequestValidator(AbstractValidator<AddVariantRequest> variantValidator, IMapper mapper)
        {
            _variantValidator = variantValidator;
            _mapper = mapper;

            RuleFor(x => x.PollId)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsGuid(x));

            RuleFor(x => x.Text)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidQuestionText(x));

            RuleFor(x => x.Type)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidQuestionType(x));

            RuleFor(x => x.Variants)
                .Must(x => x.All(item => _variantValidator.Validate(_mapper.Map<AddVariantRequest>(item)).IsValid));
        }
    }
}

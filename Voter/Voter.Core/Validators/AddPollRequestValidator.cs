using AutoMapper;
using FluentValidation;
using System.Linq;
using Voter.Dto.Requests.Poll;
using Voter.Dto.Requests.Question;

namespace Voter.Core.Validators
{
    public class AddPollRequestValidator : AbstractValidator<AddPollRequest>
    {
        private readonly AbstractValidator<AddQuestionRequest> _questionValidator;
        private readonly IMapper _mapper;
        public AddPollRequestValidator(AbstractValidator<AddQuestionRequest> questionValidator, IMapper mapper)
        {
            _questionValidator = questionValidator;
            _mapper = mapper;

            RuleFor(x => x.Name)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidName(x));

            RuleFor(x => x.Description)
                .NotNull().NotEmpty()
                .Must(x => UtilityValidationMethods.IsValidDescription(x));

            RuleFor(x => x.Questions)
                .Must(x => x.All(item =>
                {
                    var res = _questionValidator.Validate(_mapper.Map<AddQuestionRequest>(item));
                    return res.IsValid;
                }));
        }

    }
}

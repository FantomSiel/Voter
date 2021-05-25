using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Exceptions;

namespace Voter.Core.Services.Pipeline
{
    public class RequestValidatorPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private AbstractValidator<TRequest> _validator;

        public RequestValidatorPipelineBehavior(AbstractValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = await _validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                var error = result.Errors.First();
                throw new ApiException(error.ErrorCode, 400, error.ErrorMessage, "");
            }

            return await next();
        }
    }
}

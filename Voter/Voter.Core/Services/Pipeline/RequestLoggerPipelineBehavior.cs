using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Voter.Core.Abstractions.Item;

namespace Voter.Core.Services.Pipeline
{
    public class RequestLoggerPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private ILogger _logger;

        public RequestLoggerPipelineBehavior(ILogger<RequestLoggerPipelineBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (request is ILoggable loggable)
            {
                _logger.LogInformation($"Processing request: {loggable.ToLog()}");
            }
            else
            {
                _logger.LogWarning($"Processing non-loggable request");
            }

            return await next();
        }
    }
}

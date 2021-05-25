using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Voter.Core.Exceptions;

namespace Voter.Service.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiException apiException)
            {
                _logger.LogWarning($"An ApiException occured: {apiException.Message}; {apiException.Details}");
            }
            else
            {
                _logger.LogError($"An error occured while proocessing the request:\n{context.Exception.StackTrace}");
            }
        }
    }
}

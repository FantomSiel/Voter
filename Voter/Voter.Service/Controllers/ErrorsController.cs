using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System;
using Voter.Core.Exceptions;
using Voter.Dto.Responses;

namespace Voter.Service.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            var response = CreateDefaultErrorResponse(exception);
            Response.StatusCode = 500;

            if (exception is ApiException apiException)
            {
                response.Code = apiException.Code;
                response.Error = apiException.Message;
                response.Details = apiException.Details;
                Response.StatusCode = apiException.HttpCode;
            }

            return response;
        }

        private ErrorResponse CreateDefaultErrorResponse(Exception exception)
        {
            return new ErrorResponse()
            {
                Code = "1000",
                Error = "Internal error",
                Details = exception.Message,
                StackTrace = exception.StackTrace
            };
        }
    }
}

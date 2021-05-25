using System;

namespace Voter.Core.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string code, int httpCode, string message, string details)
            : base(message)
        {
            Code = code;
            HttpCode = httpCode;
            Details = details;
        }

        public string Code { get; set; }
        public int HttpCode { get; set; }
        public string Details { get; set; }
    }
}

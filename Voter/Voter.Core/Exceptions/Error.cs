using System;

namespace Voter.Core.Exceptions
{
    public class Error
    {
        public static ApiException GetException(ErrorType type, string details)
        {
            switch (type)
            {
                case ErrorType.QuestionNotFound:
                    return new ApiException("1001", 404, "No such question found", details);
                case ErrorType.QuestionAlreadyExists:
                    return new ApiException("1002", 400, "Question with such text already exists", details);
                case ErrorType.VariantNotFound:
                    return new ApiException("2001", 404, "No such variant found", details);
                case ErrorType.VariantAlreadyExists:
                    return new ApiException("2002", 400, "Variant with such text already exists", details);
                case ErrorType.PollNotFound:
                    return new ApiException("3001", 404, "No such poll found", details);
                case ErrorType.PollAlreadyExists:
                    return new ApiException("3002", 400, "Poll with such name already exists", details);
                case ErrorType.Unauthorized:
                    return new ApiException("4000", 401, "User is not authorized", details);
                case ErrorType.UserAlreadyExists:
                    return new ApiException("4002", 400, "User with such mail already exists", details);
                case ErrorType.UserAlreadyCompletePoll:
                    return new ApiException("5000", 400, "User already complete the poll", details);
                default:
                    throw new InvalidOperationException();
            }
        }
    }

    public enum ErrorType
    {
        QuestionNotFound,
        QuestionAlreadyExists,
        VariantNotFound,
        VariantAlreadyExists,
        PollNotFound,
        PollAlreadyExists,
        Unauthorized,
        UserAlreadyExists,
        UserAlreadyCompletePoll,
    }
}

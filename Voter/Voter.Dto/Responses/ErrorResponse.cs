namespace Voter.Dto.Responses
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Error { get; set; }
        public string Details { get; set; }
        public string StackTrace { get; set; }
    }
}

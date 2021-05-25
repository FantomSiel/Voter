namespace Voter.Dto.Responses.User
{
    public class SignInResponse
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
    }
}

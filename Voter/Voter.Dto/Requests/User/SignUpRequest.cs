using MediatR;

namespace Voter.Dto.Requests.User
{
    public class SignUpRequest : IRequest<Unit>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string Sex { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}

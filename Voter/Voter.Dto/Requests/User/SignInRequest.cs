using MediatR;
using Voter.Dto.Responses.User;

namespace Voter.Dto.Requests.User
{
    public class SignInRequest : IRequest<SignInResponse>
    {
        public string Header { get; set; }
    }
}

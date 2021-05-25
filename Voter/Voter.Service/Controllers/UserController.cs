using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voter.Dto.Requests.User;
using Voter.Service.Filters;

namespace Voter.Service.Controllers
{
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("vote/[controller]")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn()
        {
            var request = new SignInRequest()
            {
                Header = Request.Headers["Authorization"]
            };

            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}

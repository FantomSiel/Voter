using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voter.Dto.Dtos;
using Voter.Dto.Requests.Poll;
using Voter.Service.Attributes;
using Voter.Service.Filters;

namespace Voter.Service.Controllers
{
    [Authorize]
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("vote/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetPollList([FromQuery] GetPollListRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPoll([FromQuery] GetPollRequest request, string id)
        {
            request.Id = id;

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePoll([FromBody] UpdatePollRequest request, string id)
        {
            request.Id = id;

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddPoll([FromBody] AddPollRequest request)
        {
            request.UserId = ((UserDto)HttpContext.Items["User"]).Id;

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoll(string id)
        {
            var request = new DeletePollRequest
            {
                Id = id
            };

            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}

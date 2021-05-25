using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voter.Dto.Requests.Question;
using Voter.Service.Attributes;
using Voter.Service.Filters;

namespace Voter.Service.Controllers
{
    [Authorize]
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("vote/[controller]")]
    public class QuestionController : Controller
    {
        private readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion([FromBody] UpdateQuestionRequest request, string id)
        {
            request.Id = id;

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] AddQuestionRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(string id)
        {
            var request = new DeleteQuestionRequest
            {
                Id = id
            };

            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}

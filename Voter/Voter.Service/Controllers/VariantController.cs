using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Voter.Dto.Requests.Variant;
using Voter.Service.Attributes;
using Voter.Service.Filters;

namespace Voter.Service.Controllers
{
    [Authorize]
    [TypeFilter(typeof(ApiExceptionFilter))]
    [Route("vote/[controller]")]
    public class VariantController : Controller
    {
        private readonly IMediator _mediator;

        public VariantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVariant([FromBody] UpdateVariantRequest request, string id)
        {
            request.Id = id;

            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddVariant([FromBody] AddVariantRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariant(string id)
        {
            var request = new DeleteVariantRequest
            {
                Id = id
            };

            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}

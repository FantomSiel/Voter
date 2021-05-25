using MediatR;

namespace Voter.Dto.Requests.Variant
{
    public class AddVariantRequest : IRequest<Unit>
    {
        public string QuestionId { get; set; }
        public string Text { get; set; }
    }
}

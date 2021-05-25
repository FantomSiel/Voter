using MediatR;

namespace Voter.Dto.Requests.Variant
{
    public class UpdateVariantRequest : IRequest<Unit>
    {
        public string Id { get; set; }
        public string Text { get; set; }
    }
}

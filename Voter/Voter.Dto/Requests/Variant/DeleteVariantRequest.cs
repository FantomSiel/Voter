using MediatR;

namespace Voter.Dto.Requests.Variant
{
    public class DeleteVariantRequest : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}

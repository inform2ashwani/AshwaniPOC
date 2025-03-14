using MediatR;
using POC.Services.Contracts.ProductResponse;

namespace POC.Services.Requests.Queries
{
    public class GetProductDetailsRequest : IRequest<ProductResponse>
    {
        public Guid ProductId { get; set; }
    }
}
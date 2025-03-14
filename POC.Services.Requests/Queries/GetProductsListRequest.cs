using MediatR;
using POC.Services.Contracts.ProductResponse;

namespace POC.Services.Requests.Queries
{
    public class GetProductsListRequest : IRequest<List<ProductResponse>>
    {
    }
}
using MediatR;
using POC.Services.Contracts.ProductResponse;

namespace POC.Services.Requests.Commands
{
    public class AddProductDetailsRequest : IRequest<AddProductResponse>
    {
        public string ProductName { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MakeYear { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
    }
}
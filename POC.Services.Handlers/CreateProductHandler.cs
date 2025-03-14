using MediatR;
using POC.Services.Contracts.ProductResponse;
using POC.Services.Repositories.Interfaces;
using POC.Services.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Services.Handlers
{
    public class CreateProductHandler : IRequestHandler<AddProductDetailsRequest, AddProductResponse>
    {
        /// <summary>
        /// Barcode Service
        /// </summary>
        private readonly IProductRepository _repository;

        /// <summary>
        /// DI
        /// </summary>
        /// <param name="repository"></param>
        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddProductResponse> Handle(AddProductDetailsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.CreateProduct(request.ProductName, request.Brand, request.MakeYear, request.Description, request.Manufacturer, request.Model);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

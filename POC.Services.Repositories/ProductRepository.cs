using POC.Logger;
using POC.Services.Contracts.ProductResponse;
using POC.Services.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Services.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ///// <summary>
        ///// DB Context
        ///// </summary>
        //private readonly PocDataContext _context;

        /// <summary>
        /// Logging service
        /// </summary>
        //private readonly Logger.ILogger _logger;

        /// <summary>
        /// DI Implementation
        /// </summary>
        /// <param name="context"></param>
        /// <param name="connectionStrings"></param>
        /// <param name="VariableService"></param>
        /// <param name="logger"></param>
        public ProductRepository(
            //TXQ5_AFT_GenericContext context, IOptions<ConnectionStrings> connectionStrings, 
            //IVariableRepository VariableService, 
            //Logger.ILogger logger
            )
        {
            //_context = context;
            //_connectionStrings = connectionStrings;
            //_variableService = VariableService;
           // _logger = logger;
        }

        public async Task<AddProductResponse> CreateProduct(string ProductName,string Brand,string MakeYear,string Description,string Manufacturer,string Model)
        {
            AddProductResponse addProductResponse = new AddProductResponse();
            Guid productId = Guid.NewGuid();
            ProductResponse productResponse = new ProductResponse
            {
              ProductId = productId,
              ProductName = ProductName,
              Brand=Brand,
              MakeYear=MakeYear,
              Description= Description,
              Manufacturer= Manufacturer,
              Model= Model
            };
            addProductResponse.Response = productId;
            //_context.FilePaths.Add(file);
            return addProductResponse;
        }

     
    }
}

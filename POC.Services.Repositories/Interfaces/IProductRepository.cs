using POC.Services.Contracts.ProductResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Services.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<AddProductResponse> CreateProduct(string ProductName, string Brand, string MakeYear, string Description, string Manufacturer, string Model);
    }
}

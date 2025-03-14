using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POC.Services.Contracts.ProductResponse
{
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }=string.Empty;
        public string Brand { get; set; }=string.Empty;
        public string Manufacturer { get; set;}=string.Empty;
        public string Description { get; set; } = string.Empty;
        public string MakeYear { get; set; }= string.Empty;
        public string Model { get; set; } = string.Empty;
    }
}

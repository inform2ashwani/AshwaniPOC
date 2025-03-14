using Microsoft.AspNetCore.Mvc;
using POC.Logger;
using POC.Services.Contracts.ProductResponse;
using POC.Services.Requests.Queries;

namespace POC.Services.Query.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ApiControllerBase
    {
        /// <summary>
        /// Logging Service
        /// </summary>
        private Logger.ILogger _logger;

        public ProductController(Logger.ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get Product details by Product Id
        /// </summary>
        /// <param name="jobID"></param>
        /// <returns></returns>
        [HttpGet("GetProduct/{ProductId}")]
        public async Task<ActionResult<List<ProductResponse>>> GetProduct(Guid ProductId)
        {
            _logger.Info($"GetProduct ProductId : {ProductId} service started");
            var request = new GetProductDetailsRequest { ProductId = ProductId };
            var result = await Mediator.Send(request);
            _logger.Info($"GetProduct ProductId : {ProductId} service completed");
            return Ok();
        }
    }
}
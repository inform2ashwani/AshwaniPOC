using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Amazon.S3;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POC.Logger;
using POC.Services.Commands.API.Models;
using POC.Services.Contracts.ProductResponse;
using POC.Services.Requests;
using POC.Services.Requests.Commands;
using POC.Services.Requests.Queries;
using System.Configuration;

namespace POC.Services.Commands.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductController : ApiControllerBase
    {
        /// <summary>
        /// Logging Service
        /// </summary>
       private Logger.ILogger _logger;

        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private string _accessKey;
        private string _secretKey;
        private DynamoDBContext _context;

        public ProductController(IMediator mediator, IConfiguration configuration,Logger.ILogger logger)
        {
            _logger = logger;
            _mediator = mediator;
            _configuration = configuration;
            _accessKey =  _configuration.GetValue<string>("AWS:AccessKey");
            _secretKey = _configuration.GetValue<string>("AWS:SecretKey");
            _context = new DynamoDBContext(new AmazonDynamoDBClient(new BasicAWSCredentials(_accessKey, _secretKey), RegionEndpoint.USEast1));
        }

        /// <summary>
        /// CreateProduct
        /// </summary>
        /// <param name="ProductName"></param>
        /// <param name="Brand"></param>
        /// <param name="MakeYear"></param>
        /// <param name="Description"></param>
        /// <param name="Manufacturer"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        [HttpPost("CQRSDemo")]
        public async Task<ActionResult<AddProductResponse>> CreateProduct(string ProductName, string Brand, string MakeYear, string Description, string Manufacturer, string Model)
        {
            _logger.Info($"Create Product service started");
            var result = await _mediator.Send(new AddProductDetailsRequest { ProductName = ProductName, Brand = Brand, MakeYear = MakeYear, Description = Description, Manufacturer = Manufacturer, Model = Model });
            _logger.Info($"Create Product service completed");
            return result;
        }
      

        /// <summary>
        /// GetS3
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetS3")]
        public async Task<ActionResult> GetS3()
        {
          
            var s3Client = new AmazonS3Client(_accessKey, _secretKey,RegionEndpoint.USEast1);
            var data = await s3Client.ListBucketsAsync();
            var buckets = data.Buckets.Select(b => { return b.BucketName; });
            return Ok(buckets);
        }

        /// <summary>
        /// GetById
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("dynamodb/getProductById/{productId}")]
        public async Task<IActionResult> GetById(string productId)
        {          
            var product = await _context.LoadAsync<Product>(productId);
            if (product == null) return NotFound();
            return Ok(product);
        }

        /// <summary>
        /// GetAllProducts
        /// </summary>
        /// <returns></returns>
        [HttpGet("dynamodb/getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {            
            var product = await _context.ScanAsync<Product>(default).GetRemainingAsync();
            return Ok(product);
        }

        [HttpPost("dynamodb/CreateNewProduct")]
        public async Task<IActionResult> CreateProduct(Product productRequest)
        { 
            var product = await _context.LoadAsync<Product>(productRequest.Id);
            if (product != null) return BadRequest($"Product with Id {productRequest.Id} Already Exists");
            await _context.SaveAsync(productRequest);
            return Ok(productRequest);
        }

        [HttpDelete("dynamodb/deleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {  
            var product = await _context.LoadAsync<Product>(productId);
            if (product == null) return NotFound();
            await _context.DeleteAsync(product);
            return NoContent();
        }

        [HttpPut("dynamodb/updateProduct")]
        public async Task<IActionResult> UpdateProduct(Product productRequest)
        {
            var product = await _context.LoadAsync<Product>(productRequest.Id);
            if (product == null) return NotFound();
            await _context.SaveAsync(productRequest);
            return Ok(productRequest);
        }
    }
}
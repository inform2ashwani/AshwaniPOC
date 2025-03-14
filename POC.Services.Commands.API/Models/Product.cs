using Amazon.DynamoDBv2.DataModel;

namespace POC.Services.Commands.API.Models
{
    public class Product
    {
        [DynamoDBHashKey("id")]
        public string Id { get; set; }

        [DynamoDBProperty("name")]
        public string? Name { get; set; }

        [DynamoDBProperty("description")]
        public string? Description { get; set; }

        [DynamoDBProperty("price")]
        public decimal Price { get; set; }
    }
}

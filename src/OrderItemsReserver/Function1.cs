using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace OrderItemsReserver
{
    public class Function1
    {
        [Function("Function1")]
        //
        public static MultiResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpExample");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            var messageJson = new StreamReader(req.Body).ReadToEnd();
            var order = string.IsNullOrWhiteSpace(messageJson)
                ? new MyOrder
                {
                    id = Guid.NewGuid().ToString(),
                    BuyerId = "fakeid",
                    ShipToAddress = new Address("323", "Konak", "Izmir", "Turkey", "12345"),
                    OrderItems = [new(new CatalogItemOrdered(999, "Fake product", "picture_uri"), 100, 3)]
                }
                : JsonSerializer.Deserialize<MyOrder>(messageJson);
            if (!string.IsNullOrWhiteSpace(messageJson))
            {
                order.id = JsonSerializer.Deserialize<IdParser>(messageJson)!.Id.ToString();
            }
            var message = "Welcome to Azure Functions!";

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString(message, Encoding.UTF8);
            response.Body.Flush();

            return new MultiResponse
            {
                Order = order!,
                HttpResponse = response
            };
            //await outputFile.WriteAsync(output, 0, output.Length);
            //return new OkObjectResult("Welcome to Azure Functions!");
        }
    }

    class IdParser
    {
        public int Id { get; set; }
    }
    //public record FakeOrder(string idMy, string Message);
    public class MultiResponse
    {
        //[BlobOutput("orders/{rand-guid}-order.json", Connection = "AzureWebJobsStorage")]
        //public required string Order { get; set; }

        [CosmosDBOutput("eshop", "orders",
            Connection = "CosmosDbConnectionSetting", PartitionKey = "/id", ContainerThroughput = 400, CreateIfNotExists = true)]
        public required MyOrder Order { get; set; }

        public required HttpResponseData HttpResponse { get; set; }
    }
}

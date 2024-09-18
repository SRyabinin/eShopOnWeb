using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace OrderItemsReserver
{
    public class Function1
    {
        [Function(nameof(Function1))]
        //
        public static MultiResponse Run([ServiceBusTrigger("orders", Connection = "ServiceBusConnection")]
        string message,

            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("Function1");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            //var order = message.Body.ToObjectFromJson<MyOrder>();
            //order.id = message.Body.ToDynamicFromJson().id;
            //var message = "Welcome to Azure Functions!";

            //var response = req.CreateResponse(HttpStatusCode.OK);

            //response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            //response.WriteString(message, Encoding.UTF8);
            //response.Body.Flush();

            return new MultiResponse
            {
                Order = message.ToString(),
                //HttpResponse = response
            };
            //await outputFile.WriteAsync(output, 0, output.Length);
            //return new OkObjectResult("Welcome to Azure Functions!");
        }
    }

    //class IdParser
    //{
    //    public int Id { get; set; }
    //}
    //public record FakeOrder(string idMy, string Message);
    public class MultiResponse
    {
        [BlobOutput("orders/{rand-guid}-order.json", Connection = "AzureWebJobsStorage")]
        public required string Order { get; set; }

        //[CosmosDBOutput("eshop", "orders",
        //    Connection = "CosmosDbConnectionSetting", PartitionKey = "/id", ContainerThroughput = 400, CreateIfNotExists = true)]
        //public required MyOrder Order { get; set; }

        //public required HttpResponseData HttpResponse { get; set; }
    }
}

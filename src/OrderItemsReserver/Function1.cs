using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace OrderItemsReserver
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        //
        public async Task<MyOutputType> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var messageJson = await new StreamReader(req.Body).ReadToEndAsync();
            //var myModel = JsonConvert.DeserializeObject<ModelDto>(messageJson);

            var response = req.CreateResponse(HttpStatusCode.OK);
            //response.WriteStringAsync(messageJson);
            //UnicodeEncoding uniencoding = new UnicodeEncoding();
            //byte[] output = uniencoding.GetBytes(messageJson);
            return new MyOutputType
            {
                Order = messageJson,
                HttpResponse = response
            };
            //await outputFile.WriteAsync(output, 0, output.Length);
            //return new OkObjectResult("Welcome to Azure Functions!");
        }
    }

    public class MyOutputType
    {
        [BlobOutput("orders/{rand-guid}-order.json", Connection = "AzureWebJobsStorage")]
        public required string Order { get; set; }

        public required HttpResponseData HttpResponse { get; set; }
    }
}

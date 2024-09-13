using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderItemsReserver;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        //services.AddApplicationInsightsTelemetryWorkerService();
        //services.ConfigureFunctionsApplicationInsights();
        // If using Kestrel:
        services.Configure<KestrelServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });

        // If using IIS:
        services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
    })
    .Build();
//var address = JsonSerializer.Serialize(new Order("fakeId", new Address("323", "Konak", "Izmir", "Turkey", "12345"), [new OrderItem(new CatalogItemOrdered(999, "Fake product", "picture_uri"), 100, 3)]));
//var xxx = JsonSerializer.Deserialize<MyOrder>(address);
//var yyy = JsonSerializer.Serialize(xxx);

host.Run();

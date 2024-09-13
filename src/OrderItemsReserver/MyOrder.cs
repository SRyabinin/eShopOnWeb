using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace OrderItemsReserver;
public class MyOrder
{
    public string id { get; set; }

    public string BuyerId { get; set; }

    public Address ShipToAddress { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public DateTimeOffset OrderDate { get; set; }
}

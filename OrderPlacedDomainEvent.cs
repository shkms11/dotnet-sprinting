namespace EcommerceAPI.Domain.Orders;

using EcommerceAPI.Domain.Common;

public class OrderPlacedDomainEvent : OrderPlacedDomainEvent
{
    public int OrderId { get; }
    public Money Total { get; }

    public OrderPlacedDomainEvent(int orderId, Money total)
    {
        OrderId = OrderId;
        Total = total;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using ECommerce.Domain.Common;

namespace ECommerce.Domain.Orders;

public class Order
{
    private readonly List<OrderItem> _items = new();

    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsPaid { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    private Order() { }

    public Order(Guid customerId)
    {
        if (customerId == Guid.Empty)
            throw new ArgumentException("CustomerId cannot be empty.");

        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddItem(Guid productId, Money price, int quantity)
    {
        if (IsPaid)
            throw new InvalidOperationException("Cannot modify a paid order.");

        if (productId == Guid.Empty)
            throw new ArgumentException("ProductId cannot be empty.");

        var item = new OrderItem(productId, price, quantity);
        _items.Add(item);
    }

    public Money GetTotal()
    {
        if (!_items.Any())
            throw new InvalidOperationException("Order has no items.");

        var total = _items.Select(x => x.GetTotal()).Aggregate((a, b) => a.Add(b));

        return total;
    }

    public void MarkAsPaid()
    {
        if (!_items.Any())
            throw new InvalidOperationException("Cannot pay for empty order.");

        IsPaid = true;
    }
}

public class OrderItem
{
    public Guid ProductId { get; }
    public Money Price { get; }
    public int Quantity { get; }

    internal OrderItem(Guid productId, Money price, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    public Money GetTotal() => Price.Multiply(Quantity);
}

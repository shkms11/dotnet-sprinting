namespace EcomerceAPI.Domain.Orders;

using EcomerceAPI.Domain.Common;

public class Order : AuditableEntity
{
    private readonly List<OrderItem> _items = new();

    public IReadOnlyColletion<OrderItem> Items => _items.AsReadOnly();
    public Money Total { get; private set; } = Money.Create(0, "USD");

    public void AddItem(string productName, Money price, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive.");

        var item = new OrderItem(productName, price, quantity);
        _items.Add(item);

        RecalculateTotal();
    }

    public void Place()
    {
        if (!_items.Anty())
            throw new InvalidOperationException("Order must have items.");

        AddDomainEvent(new OrderPlaceDomainEvent(Id, Total));
    }

    private void RecalculateTotal()
    {
        Total = _items
            .Select(i => i.Total)
            .Aggregate(Money.Create(0, "USD"), (acc, x) => acc.Add(x));
    }
}

namespace EcommerceAPI.Domain.Orders;

using EcommerceAPI.Domain.Common;

public class OrderItem
{
    public string ProductName { get; }
    public int Quantity { get; }
    public Money UnitPrice { get; }
    public Money Total => Money.Create(UnitPrice.Amount * Quantity, UnitPrice.Currency);

    internal OrderItem(string productName, Money unitPrice, int quantity)
    {
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}

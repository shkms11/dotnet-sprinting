namespace EcommerceAPI.Application.Orders;

using EcommerceAPI.Domain.Common;
using EcommerceAPI.Domain.Orders;

public class PlaceOrderCommandHandler
{
    private readonly IOrderRepository _repository;

    public PlaceOrderCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle()
    {
        var order = new Order();

        order.AddItem("Laptop", Money.Create(1200, "USD"), 1);
        order.AddItem("Mouse", Money.Create(50, "USD"), 2);

        order.Place();

        await _repository.AddAsync(order);

        return order.Id;
    }
}

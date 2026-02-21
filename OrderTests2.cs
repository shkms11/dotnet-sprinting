using System;
using ECommerce.Domain.Common;
using ECommerce.Domain.Orders;
using FluentAssertions;
using Xunit;

namespace ECommerce.Domain.Tests.Orders;

public class OrderTests
{
    [Fact]
    public void Should_Create_Order()
    {
        var order = new Order(Guid.NewGuid());

        order.IsPaid.Should().BeFalse();
        order.Items.Should().BeEmpty();
    }

    [Fact]
    public void Should_Add_Item()
    {
        var order = new Order(Guid.NewGuid());
        var price = Money.Create(100, "USD");

        order.AddItem(Guid.NewGuid(), price, 2);

        order.Items.Should().HaveCount(1);
    }

    [Fact]
    public void Should_Calculate_Total()
    {
        var order = new Order(Guid.NewGuid());

        order.AddItem(Guid.NewGuid(), Money.Create(100, "USD"), 2);
        order.AddItem(Guid.NewGuid(), Money.Create(50, "USD"), 1);

        var total = order.GetTotal();

        total.Amount.Should().Be(250);
    }

    [Fact]
    public void Should_Not_Allow_Modification_After_Payment()
    {
        var order = new Order(Guid.NewGuid());
        order.AddItem(Guid.NewGuid(), Money.Create(100, "USD"), 1);
        order.MarkAsPaid();

        Action act = () => order.AddItem(Guid.NewGuid(), Money.Create(50, "USD"), 1);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Not_Allow_Payment_Without_Items()
    {
        var order = new Order(Guid.NewGuid());

        Action act = () => order.MarkAsPaid();

        act.Should().Throw<InvalidOperationException>();
    }
}
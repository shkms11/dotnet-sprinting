using ECommerce.Domain.Common;
using FluentAssertions;
using Xunit;

namespace ECommerce.Domain.Tests.Common;

public class MoneyTests
{
    [Fact]
    public void Should_Create_Money()
    {
        var money = Money.Create(100, "usd");

        money.Amount.Should().Be(100);
        money.Currency.Should().Be("USD");
    }

    [Fact]
    public void Should_Add_Money()
    {
        var m1 = Money.Create(50, "USD");
        var m2 = Money.Create(20, "USD");

        var result = m1.Add(m2);

        result.Amount.Should().Be(70);
    }

    [Fact]
    public void Should_Throw_When_Adding_Different_Currencies()
    {
        var m1 = Money.Create(50, "USD");
        var m2 = Money.Create(20, "EUR");

        Action act = () => m1.Add(m2);

        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Should_Compare_By_Value()
    {
        var m1 = Money.Create(100, "USD");
        var m2 = Money.Create(100, "USD");

        m1.Should().Be(m2);
    }
}

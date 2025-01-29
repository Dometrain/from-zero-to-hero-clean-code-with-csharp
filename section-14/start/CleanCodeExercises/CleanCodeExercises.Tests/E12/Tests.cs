using System.Data;

namespace CleanCodeExercises.Tests.E12;

public class Tests
{
    [Fact]
    public void Calculate_total_price_without_coupon()
    {
        var order = new Order(null, new[]
        {
            new OrderLine("P1", 1, 10),
            new OrderLine("P1", 2, 5)
        });

        Assert.Equal(20, order.GetTotal());
    }

    [Fact]
    public void Calculate_total_price_with_10_percent_discount_coupon()
    {
        var order = new Order(new DiscountCoupon(0.1M), new[]
        {
            new OrderLine("P1", 1, 10),
            new OrderLine("P1", 2, 5)
        });

        Assert.Equal(18, order.GetTotal());
    }
}
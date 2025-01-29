using System.Globalization;
using FluentAssertions;
using Parking.Api.Domain;

namespace Parking.Tests.Domain;

public class PricingTableSpecification
{
    [Fact]
    public void Should_throw_argument_null_exception_if_price_tiers_is_null()
    {
        var create = () => new PricingTable(null!, new NoMaxDailyPrice());

        create.Should().ThrowExactly<ArgumentNullException>();
    }

    [Fact]
    public void Should_throw_argument_exception_if_has_no_price_tiers()
    {
        var create = () => new PricingTable(
            Array.Empty<PriceTier>(), new NoMaxDailyPrice());

        create.Should().ThrowExactly<ArgumentException>()
            .WithParameterName(nameof(PricingTable.Tiers))
            .WithMessage("Missing Pricing Tiers*");
    }

    [Fact]
    public void Should_have_one_tier_when_created_with_one()
    {
        var pricingTable = new PricingTable(new[] { CreatePriceTier() }, new NoMaxDailyPrice());

        pricingTable.Tiers.Should().HaveCount(1);
    }

    [Fact]
    public void Should_generate_an_id()
    {
        var pricingTable = new PricingTable(new[] { CreatePriceTier() }, new NoMaxDailyPrice());

        pricingTable.Id.Should()
            .NotBeEmpty();
    }

    [Fact]
    public void Price_tiers_should_be_ordered_by_hour_limit()
    {
        var pricingTable = new PricingTable(new[]
        {
            CreatePriceTier(hourLimit: 24),
            CreatePriceTier(hourLimit: 4)
        }, new NoMaxDailyPrice());

        pricingTable.Tiers.Should().BeInAscendingOrder(
            tier => tier.HourLimit);
    }

    [Theory]
    [InlineData(2, 1, 25)]
    [InlineData(3, 2, 49)]
    public void Maximum_daily_price_should_be_calculated_using_tiers_if_not_defined(decimal price1, decimal price2,
        decimal maxPrice)
    {
        var pricingTable = new PricingTable(new[]
        {
            CreatePriceTier(hourLimit: 1, price: price1),
            CreatePriceTier(hourLimit: 24, price: price2)
        }, priceLimit: new NoMaxDailyPrice());

        pricingTable.GetMaxDailyPrice().Should().Be(maxPrice);
    }

    [Fact]
    public void Should_be_able_to_set_maximum_daily_price()
    {
        const int maxDailyPrice = 15;
        var pricingTable = new PricingTable(
            new[]
            {
                CreatePriceTier(hourLimit: 24, price: 1)
            }, priceLimit: new MaxDailyPrice(maxDailyPrice)
        );

        pricingTable.GetMaxDailyPrice()
            .Should().Be(maxDailyPrice);
    }

    [Fact]
    public void Should_fail_if_tiers_do_not_cover_24h()
    {
        var create = () => new PricingTable(new[]
        {
            CreatePriceTier(hourLimit: 20)
        }, new NoMaxDailyPrice());

        create.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Should_print_table_with_tiers()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-PT");
        CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-PT");

        var pricingTable = new PricingTable(new[]
        {
            CreatePriceTier(hourLimit: 1, price: 0.5m),
            CreatePriceTier(hourLimit: 24, price: 3)
        }, new NoMaxDailyPrice());

        pricingTable.ToString()
            .Should().Be(
                """
                <= 1 hours | €0,50
                <= 24 hours | €3,00
                """);
    }

    private static PriceTier CreatePriceTier(int hourLimit = 24, decimal price = 1)
        => new(hourLimit, price);
}
using FluentAssertions;
using NSubstitute;
using Parking.Api;
using Parking.Api.Domain;
using Parking.Api.TicketPrice;

namespace Parking.Tests.TicketPrice;

public class TicketPriceServiceSpecification
{
    [Fact]
    public async Task Should_return_price_for_valid_request()
    {
        await using var application = new PricingApplication();
        var pricingDbContext = application.CreatePricingDbContext();
        await using var db = pricingDbContext;
        var pricingTable = new PricingTable(new[] 
            { new PriceTier(24, 10) }, new NoMaxDailyPrice());
        await ApplyPricingTable(db, pricingTable);
        var request = CreateRequest(1);
        var service = new TicketPriceService(db, 
            new PriceCalculator());

        var ticketPriceResponse = await service.HandleAsync(
            request,
            default
        );

        ticketPriceResponse.Price.Should().Be(10);
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-19)]
    public async Task Should_throw_argument_exception_when_entry_time_eq_or_gt_exit(int duration)
    {
        await using var application = new PricingApplication();
        await using var db = application.CreatePricingDbContext();
        var service = new TicketPriceService(db, new PriceCalculator());

        var handle = () => service.HandleAsync(
            CreateRequest(duration),
            default
        );

        await handle.Should().ThrowAsync<ArgumentException>();
    }

    private static TicketPriceRequest CreateRequest(int durationInMinutes = 5)
    {
        var entry = DateTimeOffset.Now;
        return new TicketPriceRequest(entry, entry.AddMinutes(durationInMinutes));
    }

    private static async Task ApplyPricingTable(PricingDbContext db, PricingTable pricingTable)
    {
        await RemovePricingTableIfExists(db);
        db.PricingTables.Add(pricingTable);
        await db.SaveChangesAsync();
    }

    private static async Task RemovePricingTableIfExists(PricingDbContext db)
    {
        if (!db.PricingTables.Any())
            return;
        db.PricingTables.Remove(db.PricingTables.First());
        await db.SaveChangesAsync();
    }
}
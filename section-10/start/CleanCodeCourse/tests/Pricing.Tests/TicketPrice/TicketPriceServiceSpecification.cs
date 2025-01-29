using FluentAssertions;
using NSubstitute;
using Pricing.Api.Domain;
using Pricing.Api.TicketPrice;

namespace Pricing.Tests.TicketPrice;

public class TicketPriceServiceSpecification
{
    private readonly IPriceCalculator _priceCalculator;
    
    public TicketPriceServiceSpecification()
    {
        _priceCalculator = Substitute.For<IPriceCalculator>();
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-19)]
    public async Task Should_throw_argument_exception_when_entry_time_eq_or_gt_exit(int duration)
    {
        await using var application = new PricingApplication();
        await using var db = application.CreatePricingDbContext();
        var service = new TicketPriceService(db, _priceCalculator);
        
        var handle = () => service.HandleAsync(
            CreateRequest(duration),
            default
        );
        
        await handle.Should().ThrowAsync<ArgumentException>();
    }
    
    private static TicketPriceRequest CreateRequest(int durationInMinutes= 5)
    {
        var entry = DateTimeOffset.Now;
        return new TicketPriceRequest(entry, entry.AddMinutes(durationInMinutes));
    }
}
namespace Pricing.Api.TicketPrice;

public record TicketPriceRequest(DateTimeOffset Entry, DateTimeOffset Exit)
{
}
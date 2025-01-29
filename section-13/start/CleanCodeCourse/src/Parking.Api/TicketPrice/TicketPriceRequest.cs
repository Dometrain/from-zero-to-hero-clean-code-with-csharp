namespace Parking.Api.TicketPrice;

public record TicketPriceRequest(DateTimeOffset Entry, DateTimeOffset Exit)
{
}
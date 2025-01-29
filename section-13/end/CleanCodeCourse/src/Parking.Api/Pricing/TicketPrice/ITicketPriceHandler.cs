namespace Parking.Api.TicketPrice;

public interface ITicketPriceHandler
{
    Task<TicketPriceResponse> HandleAsync(TicketPriceRequest request, CancellationToken cancellationToken);
}
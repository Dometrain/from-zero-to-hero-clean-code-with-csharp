using Parking.Api.Domain;

namespace Parking.Api.TicketPrice;

public interface IPriceCalculator
{
    decimal Calculate(PricingTable pricingTable, TicketPriceRequest ticketPriceRequest);
}
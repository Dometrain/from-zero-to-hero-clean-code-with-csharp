using Pricing.Api.Domain;

namespace Pricing.Api.TicketPrice;

public interface IPriceCalculator
{
    decimal Calculate(PricingTable pricingTable, TicketPriceRequest ticketPriceRequest);
}
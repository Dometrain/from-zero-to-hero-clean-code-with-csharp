using Pricing.Api.Domain;
using Pricing.Api.TicketPrice.Extensions;

namespace Pricing.Api.TicketPrice;

/* CHANGES
 * -----------
 * 2023-05-12: Bug Fix - price for upper tier
 * 2023-09-14: Support max daily price
 * 2023-10-01: Bug Fix - error when max daily price is not set
 */
public class PriceCalculator : IPriceCalculator
{
    public decimal Calculate(PricingTable pricingTable, TicketPriceRequest ticketPriceRequest)
    {
        var price = 0m;
        var ticketHoursToPay = ticketPriceRequest
            .GetDurationInHours();
        
        foreach (var tier in pricingTable.Tiers)
        {
            // Calculate the price for the current tier
            var hoursInTier = Math.Min(tier.HourLimit, ticketHoursToPay);
            price +=  tier.Price * hoursInTier;
            
            ticketHoursToPay -= tier.HourLimit;
            if (ticketHoursToPay <= 0)
                break;
        }
        
        // When the calculated price for the stay
        // is higher than the defined max daily price
        // we apply the MaxDailyPrice
        return Math.Min(price, pricingTable.GetMaxDailyPrice());
    }
}
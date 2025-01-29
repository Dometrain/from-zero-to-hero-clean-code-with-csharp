using Parking.Api.Domain;
using Parking.Api.Domain.PriceLimit;
using Parking.Api.Pricing.ApplyPricing;

namespace Parking.Api.Extensions;

internal static class RequestToDomainMapper
{
    public static PricingTable ToPricingTable(this ApplyPricingRequest request)
    {
        return new PricingTable(
            request.Tiers.Select(tier => new PriceTier(tier.HourLimit, tier.Price)),
            new MaxDailyPrice(100)
        );
    }
}
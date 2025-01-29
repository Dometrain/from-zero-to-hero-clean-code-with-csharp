using Parking.Api.ApplyPricing;
using Parking.Api.Domain;

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
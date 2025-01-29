using Pricing.Api.ApplyPricing;
using Pricing.Api.Domain;

namespace Pricing.Api.Extensions;

internal static class RequestToDomainMapper
{
    public static PricingTable ToPricingTable(this ApplyPricingRequest request)
    {
        return new PricingTable(
            request.Tiers.Select(tier => new PriceTier(tier.HourLimit, tier.Price))
        );
    }
}
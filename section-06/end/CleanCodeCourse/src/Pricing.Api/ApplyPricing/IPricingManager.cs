namespace Pricing.Api.ApplyPricing;

public interface IPricingManager
{
    Task<bool> HandleAsync(ApplyPricingRequest request, CancellationToken cancellationToken);
}
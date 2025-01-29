namespace Parking.Api.Pricing.ApplyPricing;

public interface IApplyPricingHandler
{
    Task<bool> HandleAsync(ApplyPricingRequest request, CancellationToken cancellationToken);
}
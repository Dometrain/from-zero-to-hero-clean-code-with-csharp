namespace Parking.Api.ApplyPricing;

public interface IApplyPricingHandler
{
    Task<bool> ApplyPricingAsync(ApplyPricingRequest request, CancellationToken cancellationToken);
}
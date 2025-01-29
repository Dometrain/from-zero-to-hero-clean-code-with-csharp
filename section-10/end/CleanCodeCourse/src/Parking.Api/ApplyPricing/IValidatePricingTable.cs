namespace Parking.Api.ApplyPricing;

public interface IValidatePricingTable
{
    bool ValidatePricingTable(ApplyPricingRequest request);
}
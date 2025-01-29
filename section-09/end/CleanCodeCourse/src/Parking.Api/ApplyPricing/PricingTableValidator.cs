using Parking.Api.Domain.Exceptions;
using Parking.Api.Extensions;

namespace Parking.Api.ApplyPricing;

public class PricingTableValidator
{
    public PricingTableValidator()
    {
    }

    public bool ValidatePricingTable(ApplyPricingRequest request)
    {
        try
        {
            if (request?.Tiers is null) 
                return false;
            
            request.ToPricingTable();
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
        catch (InvalidPricingTierException)
        {
            return false;
        }
    }
}
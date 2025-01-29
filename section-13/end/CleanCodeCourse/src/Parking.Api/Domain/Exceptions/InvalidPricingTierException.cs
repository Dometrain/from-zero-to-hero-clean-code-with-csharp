namespace Parking.Api.Domain.Exceptions;

public class InvalidPricingTierException : Exception
{
    public InvalidPricingTierException(string message) : base(message)
    {
    }
}
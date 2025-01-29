using System.ComponentModel.DataAnnotations.Schema;
using Parking.Api.Domain.Exceptions;

namespace Parking.Api.Domain;

[ComplexType]
public record PriceTier
{
    public PriceTier(int hourLimit, decimal price)
    {
        if (hourLimit is < 1 or > 24)
            throw new InvalidPricingTierException();
        if (price < 0)
            throw new InvalidPricingTierException();

        HourLimit = hourLimit;
        Price = price;
    }

    public decimal Price { get; }
    public int HourLimit { get; }

    public override string ToString()
    {
        return $"<= {HourLimit} hours | {Price:C}";
    }

    public decimal CalculateFullPrice()
    {
        return Price * HourLimit;
    }
}
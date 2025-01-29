namespace Parking.Api.Domain.PriceLimit;

public interface IDayPriceLimit
{
    public decimal GetMaxPriceForOneDay(PricingTable table);
}
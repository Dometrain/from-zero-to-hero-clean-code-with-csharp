namespace Parking.Api.Domain;

public interface IDayPriceLimit
{
    public decimal GetMaxPriceForOneDay(PricingTable table);
}
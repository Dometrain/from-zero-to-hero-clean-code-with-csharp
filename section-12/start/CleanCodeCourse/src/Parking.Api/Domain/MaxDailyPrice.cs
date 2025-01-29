namespace Parking.Api.Domain;

public class MaxDailyPrice : IDayPriceLimit
{
    private decimal _maxDailyPrice;

    public MaxDailyPrice(decimal maxDailyPrice)
    {
        _maxDailyPrice = maxDailyPrice;
    }

    public decimal GetMaxPriceForOneDay(PricingTable table)
    {
        return _maxDailyPrice;
    }
}
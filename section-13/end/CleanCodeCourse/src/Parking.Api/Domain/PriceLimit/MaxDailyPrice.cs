namespace Parking.Api.Domain.PriceLimit;

public class MaxDailyPrice : IDayPriceLimit
{
    private readonly decimal _maxDailyPrice;

    public MaxDailyPrice(decimal maxDailyPrice)
    {
        _maxDailyPrice = maxDailyPrice;
    }

    public decimal GetMaxPriceForOneDay(PricingTable table)
    {
        return _maxDailyPrice;
    }
}
namespace Parking.Api.Domain;

public class NoMaxDailyPrice : IDayPriceLimit
{
    public decimal GetMaxPriceForOneDay(PricingTable table)
    {
        return CalculateMaxDailyPriceFromTiers(table);
    }

    private decimal CalculateMaxDailyPriceFromTiers(PricingTable table)
    {
        decimal total = 0;
        var hoursIncluded = 0;
        foreach (var tier in table.Tiers)
        {
            total += tier.Price * (tier.HourLimit - hoursIncluded);
            hoursIncluded = tier.HourLimit;
        }

        return total;
    }
}
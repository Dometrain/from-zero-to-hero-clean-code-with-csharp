namespace Parking.Api.Domain.PriceLimit;

public class NoMaxDailyPrice : IDayPriceLimit
{
    public decimal GetMaxPriceForOneDay(PricingTable table)
    {
        return CalculateUsingTiers(table);
    }

    private static decimal CalculateUsingTiers(PricingTable table)
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
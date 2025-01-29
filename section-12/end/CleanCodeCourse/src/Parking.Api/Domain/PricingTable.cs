using System.Collections.Immutable;

namespace Parking.Api.Domain;

public class PricingTable
{
    private readonly IDayPriceLimit _priceLimit;
    private readonly Guid _id;

    private PricingTable()
    {
    }

    public PricingTable(IEnumerable<PriceTier> tiers, IDayPriceLimit priceLimit)
    {
        _priceLimit = priceLimit;
        _id = Guid.NewGuid();
        Tiers = tiers?.OrderBy(tier => tier.HourLimit).ToImmutableArray() ?? throw new ArgumentNullException();

        if (!Tiers.Any())
            throw new ArgumentException("Missing Pricing Tiers", nameof(Tiers));

        if (Tiers.Last().HourLimit < 24) throw new ArgumentException();
    }

    public IReadOnlyCollection<PriceTier> Tiers { get; }
    public Guid Id => _id;

    public decimal GetMaxDailyPrice()
        => _priceLimit.GetMaxPriceForOneDay(this);

    public IEnumerable<PriceTier> GetPriceTierThatCostMoreThan(decimal price)
    {
        if (price < 1)
            return Enumerable.Empty<PriceTier>();

        return Tiers
            .Where(tier => tier.Price > price)
            .ToList();
    }

    public override string ToString()
    {
        return string.Join(
            Environment.NewLine,
            Tiers.Select(tier => tier.ToString()));
    }
}
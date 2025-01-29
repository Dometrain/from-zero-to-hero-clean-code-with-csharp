using System.Collections.Immutable;
using Parking.Api.Domain.PriceLimit;

namespace Parking.Api.Domain;

public class PricingTable
{
    private readonly IDayPriceLimit _priceLimit;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private PricingTable() // Needed for EF Core Serialization
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public PricingTable(IEnumerable<PriceTier> tiers, IDayPriceLimit priceLimit)
    {
        _priceLimit = priceLimit;
        Id = Guid.NewGuid();
        Tiers = tiers?.OrderBy(tier => tier.HourLimit).ToImmutableArray() ?? throw new ArgumentNullException();

        if (!Tiers.Any())
            throw new ArgumentException("Missing Pricing Tiers", nameof(Tiers));

        if (Tiers.Last().HourLimit < 24) 
	        throw new ArgumentException("Last tier needs to have 24 hours as a limit");
    }

    public IReadOnlyCollection<PriceTier> Tiers { get; }
    public Guid Id { get; }

    public decimal GetMaxDailyPrice()
        => _priceLimit.GetMaxPriceForOneDay(this);

    public override string ToString()
    {
        return string.Join(
            Environment.NewLine,
            Tiers.Select(tier => tier.ToString()));
    }
}
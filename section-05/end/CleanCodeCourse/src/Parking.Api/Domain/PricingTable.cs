using System.Collections.Immutable;

namespace Parking.Api.Domain;

public class PricingTable
{
    private readonly decimal? _maxDailyPrice;
    private readonly Guid _id;

    public PricingTable()
    {
    }

    public PricingTable(IEnumerable<PriceTier> tiers)
        : this(tiers, null!)
    {
    }

    public PricingTable(IEnumerable<PriceTier> tiers, decimal? maxDailyPrice)
    {
        _id = Guid.NewGuid();
        _maxDailyPrice = maxDailyPrice;
        Tiers = tiers?.OrderBy(tier => tier.HourLimit).ToImmutableArray() ?? throw new ArgumentNullException();

        if (!Tiers.Any())
            throw new ArgumentException("Missing Pricing Tiers", nameof(Tiers));

        if (Tiers.Last().HourLimit < 24) throw new ArgumentException();
    }

    public IReadOnlyCollection<PriceTier> Tiers { get; }
    public Guid Id => _id;

    public decimal GetMaxDailyPrice()
    {

        var calculatedPrice = 0M;
        if (_maxDailyPrice != null)
        {
            calculatedPrice = _maxDailyPrice.Value;
        }
        else
        {
            decimal total = 0;
            var hoursIncluded = 0;
            foreach (var tier in Tiers)
            {
                total += tier.Price * (tier.HourLimit - hoursIncluded);
                hoursIncluded = tier.HourLimit;
            }

            calculatedPrice = total;
        }

        return calculatedPrice;
    }

    public override string ToString()
    {
        return string.Join(
            Environment.NewLine,
            Tiers.Select(tier => tier.ToString()));
    }
}
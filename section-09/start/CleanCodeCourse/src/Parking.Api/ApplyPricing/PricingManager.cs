using Ardalis.GuardClauses;
using Parking.Api.Domain;
using Parking.Api.Extensions;

namespace Parking.Api.ApplyPricing;

public class PricingManager : IPricingManager
{
    private readonly PricingDbContext _dbContext;

    public PricingManager(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HandleAsync(ApplyPricingRequest request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request);
        Guard.Against.Null(request.Tiers);

        await SaveAsync(request.ToPricingTable(), cancellationToken);
        
        return true;
    }

    private async Task SaveAsync(PricingTable pricingTable, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(pricingTable, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
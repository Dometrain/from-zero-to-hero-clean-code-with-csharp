using Pricing.Api.Extensions;

namespace Pricing.Api.ApplyPricing;

public class PricingManager : IPricingManager
{
    private readonly PricingDbContext _dbContext;

    public PricingManager(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HandleAsync(ApplyPricingRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var pricingTable = request.ToPricingTable();
        
        await _dbContext.AddAsync(pricingTable, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
using Microsoft.EntityFrameworkCore;

namespace Pricing.Api.TicketPrice;

public class TicketPriceService : ITicketPriceService
{
    private readonly PricingDbContext _dbContext;
    private readonly IPriceCalculator _pricingCalculator;

    public TicketPriceService(PricingDbContext dbContext, IPriceCalculator pricingCalculator)
    {
        _dbContext = dbContext;
        _pricingCalculator = pricingCalculator;
    }

    public async Task<TicketPriceResponse> HandleAsync(TicketPriceRequest request, CancellationToken cancellationToken)
    {
        if (request.Entry >= request.Exit)
            throw new ArgumentException();

        var pricingTable = await _dbContext.PricingTables.FirstOrDefaultAsync(cancellationToken);

        var price = _pricingCalculator.Calculate(pricingTable, request);
        return new TicketPriceResponse(price);
    }
}
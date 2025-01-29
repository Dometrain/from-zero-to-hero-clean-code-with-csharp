using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Domain;

namespace Parking.Api.TicketPrice;

public class TicketPriceHandler : ITicketPriceHandler
{
    private readonly PricingDbContext _dbContext;
    private readonly PriceCalculator _pricingCalculator;

    public TicketPriceHandler(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
        _pricingCalculator = new PriceCalculator();
    }

    public async Task<TicketPriceResponse> HandleAsync(TicketPriceRequest request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request, nameof(request), "Request can't be null");
        if (request.Entry >= request.Exit)
	        throw new ArgumentException("Entry should be before exit");
        
        var pricingTable = await GetPricingTable(cancellationToken);
        return BuildPriceResponse(request, pricingTable);
    }

    private async Task<PricingTable> GetPricingTable(CancellationToken cancellationToken)
    {
        return await _dbContext.PricingTables.FirstOrDefaultAsync(cancellationToken)
	        ?? new NoPricingTable();
    }

    private TicketPriceResponse BuildPriceResponse(TicketPriceRequest request, PricingTable pricingTable)
    {
        var price = _pricingCalculator.Calculate(pricingTable, request);
        return new TicketPriceResponse(price);
    }
}
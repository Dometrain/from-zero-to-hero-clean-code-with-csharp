using Microsoft.EntityFrameworkCore;
using Parking.Api.Domain;

namespace Parking.Api.TicketPrice;

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
        ValidateRequest(request);
        var pricingTable = await GetPricingTable(cancellationToken);
        return CalculatePriceResponse(request, pricingTable);
    }

    private async Task<PricingTable?> GetPricingTable(CancellationToken cancellationToken)
    {
        var pricingTable = await _dbContext.PricingTables.FirstOrDefaultAsync(cancellationToken);
        return pricingTable;
    }

    private static void ValidateRequest(TicketPriceRequest request)
    {
        if (request.Entry >= request.Exit)
            throw new ArgumentException();
    }

    private TicketPriceResponse CalculatePriceResponse(TicketPriceRequest request, PricingTable? pricingTable)
    {
        var price = _pricingCalculator.Calculate(pricingTable, request);
        return new TicketPriceResponse(price);
    }
}
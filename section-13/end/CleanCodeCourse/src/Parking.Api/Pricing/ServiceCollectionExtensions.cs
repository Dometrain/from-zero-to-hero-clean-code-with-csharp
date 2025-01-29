using Parking.Api.Pricing.ApplyPricing;
using Parking.Api.TicketPrice;

namespace Parking.Api;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPricing(this IServiceCollection services)
	{
		services.AddScoped<IApplyPricingHandler, ApplyPricingHandler>();
		services.AddScoped<ITicketPriceHandler, TicketPriceHandler>();
		return services;
	}
}
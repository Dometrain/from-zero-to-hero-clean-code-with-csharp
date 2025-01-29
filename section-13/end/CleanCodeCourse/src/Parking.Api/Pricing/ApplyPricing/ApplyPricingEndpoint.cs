using Parking.Api.Pricing.ApplyPricing;

namespace Parking.Api.ApplyPricing;

public static class ApplyPricingEndpoint
{
	public static async Task<IResult> HandleAsync(
		IApplyPricingHandler handler,
		ApplyPricingRequest request,
		CancellationToken cancellationToken)
	{
		try
		{
			return await ApplyPricingAsync(handler, request, cancellationToken);
		}
		catch (ArgumentNullException)
		{
			return Results.BadRequest();
		}
		catch (Exception)
		{
			return Results.Problem();
		}
	}

	private static async Task<IResult> ApplyPricingAsync(IApplyPricingHandler handler,
		ApplyPricingRequest applyPricingRequest,
		CancellationToken cancellationToken1)
	{
		var result = await handler.HandleAsync(applyPricingRequest, cancellationToken1);

		return result ? Results.Ok() : Results.BadRequest();
	}
}
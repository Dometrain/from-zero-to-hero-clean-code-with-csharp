namespace Parking.Api.Domain.Exceptions;

public static class InvalidPricingTierExceptionMessages
{
	public static string PriceNegative => "Price can't negative";
	public static string HourLimitOutOfRange => "Hour limit should be between 1 and 24";
}
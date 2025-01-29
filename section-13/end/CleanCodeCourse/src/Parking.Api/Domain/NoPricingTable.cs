using Parking.Api.Domain.PriceLimit;

namespace Parking.Api.Domain;

public class NoPricingTable : PricingTable
{
	public NoPricingTable() 
		: base(new PriceTier[]{new PriceTier(24, 0)}, new NoMaxDailyPrice())
	{
	}
}
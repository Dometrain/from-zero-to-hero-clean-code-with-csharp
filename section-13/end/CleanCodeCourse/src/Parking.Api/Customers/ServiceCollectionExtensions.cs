using Parking.Api.Customers.Add;

namespace Parking.Api.Customers;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddCustomers(this IServiceCollection services)
	{
		services.AddScoped<IAddCustomer, AddCustomerHandler>();
		return services;
	}
}
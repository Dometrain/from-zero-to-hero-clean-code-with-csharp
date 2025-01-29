using Parking.Api.Domain;

namespace Parking.Api.Customers.Add.Extensions;

public static class AddCustomerRequestExtensions
{
	public static Customer Map(this AddCustomerRequest request)
	{
		var address = new Address(request.Street, request.ZipCode, request.City, request.Country);

		var customer = new Customer(
			request.Name,
			false,
			address);

		foreach (var licensePlate in request.LicensePlates)
		{
			customer.AddLicensePlate(new LicensePlate(licensePlate));
		}

		return customer;
	}
}
using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using Parking.Api.Customers.Add;
using Parking.Api.Domain;

namespace Parking.Api.Customers;

public class AddCustomerHandler : IAddCustomer
{
	private readonly PricingDbContext _dbContext;
	private readonly CustomerValidator _validator;

	public AddCustomerHandler(PricingDbContext dbContext)
	{
		_dbContext = dbContext;
		_validator = new CustomerValidator();
	}

	public async Task<AddCustomerResponse> HandleAsync(AddCustomerRequest request, CancellationToken cancellationToken)
	{
		return await CreateCustomerAsync(request, cancellationToken);
	}

	private async Task<AddCustomerResponse> CreateCustomerAsync(AddCustomerRequest request,
		CancellationToken cancellationToken)
	{
		var address = new Address(request.Street, request.ZipCode, request.City, request.Country);
		var customer = CreateCustomer(request.Name, request.LicensePlates, address);

		if (!_validator.IsValid(customer))
			throw new ArgumentException("Invalid Customer");

		await SaveAsync(customer, cancellationToken);
		return new AddCustomerResponse(customer.Id, customer.CreatedOn);
	}

	private Customer CreateCustomer(string name, string[] licensePlates,
		Address address)
	{
		var customer = new Customer(
			name,
			false,
			address);

		foreach (var licensePlate in licensePlates)
		{
			customer.AddLicensePlate(new LicensePlate(licensePlate));
		}

		return customer;
	}

	private async Task<bool> SaveAsync(Customer customer, CancellationToken cancellationToken)
	{
		await _dbContext.AddAsync(customer, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
		return true;
	}
}
using Parking.Api.Customers.Add.Extensions;
using Parking.Api.Domain;

namespace Parking.Api.Customers.Add;

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
		var customer = request.Map();

		if (!_validator.IsValid(customer))
			throw new ArgumentException("Invalid Customer");

		await SaveAsync(customer, cancellationToken);
		return new AddCustomerResponse(customer.Id, customer.CreatedOn);
	}

	private async Task SaveAsync(Customer customer, CancellationToken cancellationToken)
	{
		await _dbContext.AddAsync(customer, cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}
}
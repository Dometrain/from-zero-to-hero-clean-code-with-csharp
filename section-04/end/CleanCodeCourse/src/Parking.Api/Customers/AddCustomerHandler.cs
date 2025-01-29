using Parking.Api.Customers.Add;

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

    private async Task<AddCustomerResponse> CreateCustomerAsync(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var createdDate = DateTime.UtcNow;
        var id = Guid.NewGuid();
        var customer = new Customer
        {
            Id = id,
            CreatedOn = createdDate,
            Name = request.Name,
            VehicleLicensePlates = request.LicensePlates,
            IsEmployee = false
        };

        _ = _validator.IsValid(customer) &&
            await AddCustomer(cancellationToken, customer);
        
        return new AddCustomerResponse(customer.Id, createdDate);
    }

    private async Task<bool> AddCustomer(CancellationToken cancellationToken, Customer customer)
    {
        await _dbContext.AddAsync(customer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
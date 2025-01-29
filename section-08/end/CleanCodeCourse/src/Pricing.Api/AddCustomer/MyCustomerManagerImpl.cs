namespace Pricing.Api.AddCustomer;

public class MyCustomerManagerImpl : IAddCustomer
{
    private readonly PricingDbContext _dbContext;
    private readonly CustomerValidator _validator;

    public MyCustomerManagerImpl(PricingDbContext dbContext)
    {
        _dbContext = dbContext;
        _validator = new CustomerValidator();
    }

    public async Task<AddCustomerResponse> ProcessAddCustomerAsync(AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var createdDate = DateTime.UtcNow;
        var idGuid = Guid.NewGuid();
        var myCustomer = new Customer
        {
            Id = idGuid,
            CustomerCreatedOn = createdDate,
            Name = request.Name,
            CustomerVLPCollection = request.LicensePlates,
            Employee = false
        };

        _ = _validator.IsValid(myCustomer) &&
                     await AddCustomer(cancellationToken, myCustomer);

        return new AddCustomerResponse(myCustomer.Id, createdDate);
    }

    private async Task<bool> AddCustomer(CancellationToken cancellationToken, Customer customer)
    {
        await _dbContext.AddAsync(customer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
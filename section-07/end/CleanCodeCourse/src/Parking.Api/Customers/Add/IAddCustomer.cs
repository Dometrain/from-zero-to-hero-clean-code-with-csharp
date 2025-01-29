namespace Parking.Api.Customers.Add;

public interface IAddCustomer
{
    public Task<AddCustomerResponse> HandleAsync(AddCustomerRequest request, CancellationToken cancellationToken);
}
namespace Parking.Api.Customers.Add;

public interface IAddCustomer
{
    public Task<AddCustomerResponse> ProcessAddCustomerAsync(AddCustomerRequest request, CancellationToken cancellationToken);
}
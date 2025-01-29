namespace Pricing.Api.AddCustomer;

public interface IAddCustomer
{
    public Task<AddCustomerResponse> ProcessAddCustomerAsync(AddCustomerRequest request, CancellationToken cancellationToken);
}
using Microsoft.AspNetCore.Http.HttpResults;

namespace Pricing.Api.AddCustomer;

public static class AddCustomerEndpoint
{
    public static async Task<Ok<AddCustomerResponse>> HandleAsync(
        AddCustomerRequest request,
        IAddCustomer handler,
        CancellationToken cancellationToken)
    {
        var result = await
            handler.ProcessAddCustomerAsync(request, cancellationToken);
        return TypedResults.Ok<AddCustomerResponse>(result);
    }
}
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Customers;
using Parking.Api.Customers.Add;
using Parking.Tests.Builders;

namespace Parking.Tests.AddCustomer;

public class AddCustomerSpecification
{
    [Fact]
    public async Task Should_add_customer_if_request_is_valid()
    {
        var request = new AddCustomerRequestBuilder()
            .WithName("John Doe")
            .AddLicensePlate("AR32DU")
            .Build();
        await using var application = new PricingApplication();
        await using var db = application.CreatePricingDbContext();
        var handler = new AddCustomerHandler(db);
        
        var response = await handler.HandleAsync(request, CancellationToken.None);
        
        response.Should().NotBeNull();
        (await db.Customers.CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task Should_fail_to_add_if_request_is_invalid()
    {
        var request = new AddCustomerRequestBuilder()
            .AddLicensePlate("ABC123")
            .AddLicensePlate("INVALID")
            .Build();
        await using var application = new PricingApplication();
        await using var db = application.CreatePricingDbContext();
        var handler = new AddCustomerHandler(db);
        
        var handle = () =>
            handler.HandleAsync(request, CancellationToken.None);
        
        await handle.Should().ThrowAsync<InvalidLicensePlateException>();
    }
}
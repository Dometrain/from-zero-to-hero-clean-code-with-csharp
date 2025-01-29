using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Parking.Api.Customers;
using Parking.Api.Customers.Add;

namespace Parking.Tests.AddCustomer;

public class AddCustomerSpecification
{
    [Fact]
    public async Task Should_add_customer_if_request_is_valid()
    {
        var request = new AddCustomerRequest("John Doe", new[] { "AA00DD" },
            string.Empty, string.Empty, string.Empty, string.Empty);
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
        var request =  new AddCustomerRequest("", new string[] { "ABC123", "XYZ789" },
            string.Empty, string.Empty, string.Empty, string.Empty);
        await using var application = new PricingApplication();
        await using var db = application.CreatePricingDbContext();
        var handler = new AddCustomerHandler(db);
        await Assert.ThrowsAsync<InvalidLicensePlateException>(() =>
            handler.HandleAsync(request, CancellationToken.None));
        (await db.Customers.CountAsync()).Should().Be(0);
    }
}

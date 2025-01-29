using Microsoft.EntityFrameworkCore;
using Parking.Api;
using Parking.Api.Customers.Add;
using Parking.Api.Domain.Exceptions;
using Parking.Tests.Builders;

namespace Parking.Tests.Customers.Add;

public class AddCustomerSpecification : IAsyncLifetime
{
    private AddCustomerHandler _handler;
    private PricingDbContext _db;

    public async Task InitializeAsync()
    {
        await using var application = new ParkingApplication();
        _db = application.CreatePricingDbContext();
        _handler = new AddCustomerHandler(_db);
    }

    [Fact]
    public async Task Should_add_customer_if_request_is_valid()
    {
        var request = new AddCustomerRequestBuilder()
            .WithName("John Doe")
            .AddLicensePlate("AR32DU")
            .Build();

        var response = await _handler.HandleAsync(request, CancellationToken.None);

        response.Should().NotBeNull();
        (await _db.Customers.CountAsync()).Should().Be(1);
    }

    [Fact]
    public async Task Should_fail_to_add_if_request_is_invalid()
    {
        var request = new AddCustomerRequestBuilder()
            .AddLicensePlate("ABC123")
            .AddLicensePlate("INVALID")
            .Build();

        var handle = () =>
            _handler.HandleAsync(request, CancellationToken.None);

        await handle.Should().ThrowAsync<InvalidLicensePlateException>();
    }

    public async Task DisposeAsync()
    {
        await _db.Database.EnsureDeletedAsync();
        await _db.DisposeAsync();
    }
}
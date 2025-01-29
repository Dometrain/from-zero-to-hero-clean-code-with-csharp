using FluentAssertions;
using Parking.Api;
using Parking.Api.ApplyPricing;

namespace Parking.Tests.ApplyPricing;

public class ApplyPricingSpecification : IAsyncLifetime
{
    private const int MaxHourLimit = 24;

    private PricingDbContext _db;

    public async Task InitializeAsync()
    {
        await using var application = new PricingApplication();
        _db = application.CreatePricingDbContext();
    }

    [Fact]
    public async Task Should_throw_argument_null_exception_if_request_is_null()
    {
        var pricingManager = new ApplyPricingHandler(_db);

        var handleRequest = () => pricingManager.ApplyPricingAsync(null!, default);

        await handleRequest.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Should_return_true_if_succeeded()
    {
        var pricingManager = new ApplyPricingHandler(_db);

        var result = await pricingManager.ApplyPricingAsync(CreateRequest(), default);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task Should_save_equivalent_data_to_storage()
    {
        var pricingManager = new ApplyPricingHandler(_db);
        var applyPricingRequest = CreateRequest();

        _ = await pricingManager.ApplyPricingAsync(applyPricingRequest, default);

        _db.PricingTables
            .FirstOrDefault()
            .Should()
            .BeEquivalentTo(applyPricingRequest);
    }

    private static ApplyPricingRequest CreateRequest()
    {
        return new ApplyPricingRequest(
            new[] { new PriceTierRequest(MaxHourLimit, 1) }
        );
    }

    public async Task DisposeAsync()
    {
        await _db.Database.EnsureDeletedAsync();
        await _db.DisposeAsync();
    }
}
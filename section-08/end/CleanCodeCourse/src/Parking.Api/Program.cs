using Microsoft.EntityFrameworkCore;
using Parking.Api;
using Parking.Api.ApplyPricing;
using Parking.Api.Customers;
using Parking.Api.Customers.Add;
using Parking.Api.TicketPrice;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddDbContext<PricingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Pricing")));

builder.Services.AddScoped<IPricingManager, PricingManager>();
builder.Services.AddScoped<ITicketPriceService, TicketPriceService>();
builder.Services.AddScoped<IPriceCalculator, PriceCalculator>();
builder.Services.AddScoped<IAddCustomer, AddCustomerHandler>();
// DI

var app = builder.Build();

#region endpoints

app.MapGet("/", () => "Parking API");
app.MapPut("/PricingTable", async (IPricingManager pricingManager,
    ApplyPricingRequest request,
    CancellationToken cancellationToken) =>
{
    try
    {
        return await ApplyPricingAsync(pricingManager, request, cancellationToken);
    }
    catch (ArgumentNullException)
    {
        return Results.BadRequest();
    }
    catch (Exception)
    {
        return Results.Problem();
    }
    
});
app.MapGet("/TicketPrice", TicketPriceEndpoint.HandleAsync);

app.MapPost("/customers", AddCustomerEndpoint.HandleAsync);

#endregion

await InitializeDatabase(app);

app.Run();
return;

async Task InitializeDatabase(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PricingDbContext>();

    await context.Database.EnsureCreatedAsync();
}

async Task<IResult> ApplyPricingAsync(IPricingManager pricingManager1, ApplyPricingRequest applyPricingRequest,
    CancellationToken cancellationToken1)
{
    var result = await pricingManager1.HandleAsync(applyPricingRequest, cancellationToken1);

    return result ? Results.Ok() : Results.BadRequest();
}
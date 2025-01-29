using Microsoft.EntityFrameworkCore;
using Pricing.Api;
using Pricing.Api.AddCustomer;
using Pricing.Api.ApplyPricing;
using Pricing.Api.TicketPrice;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddDbContext<PricingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Pricing")));

builder.Services.AddScoped<IPricingManager, PricingManager>();
builder.Services.AddScoped<ITicketPriceService, TicketPriceService>();
builder.Services.AddScoped<IPriceCalculator, PriceCalculator>();
// DI

var app = builder.Build();

#region endpoints

app.MapGet("/", () => "Hello World!");
app.MapPut("/PricingTable", async (IPricingManager pricingManager,
    ApplyPricingRequest request,
    CancellationToken cancellationToken) =>
{
    try
    {
        var result = await pricingManager.HandleAsync(request, cancellationToken);
        if (result)
        {
            return Results.Ok();
        } // if
        else
        {
            return Results.BadRequest();
        } // else
    } // try
    catch (Exception)
    {
        return Results.Problem();
    } //catch
});
app.MapGet("/TicketPrice", TicketPriceEndpoint.HandleAsync);

app.MapPost("/customers", AddCustomerEndpoint.HandleAsync);

#endregion

// Initialize Database - Begin
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<PricingDbContext>();

await context.Database.EnsureCreatedAsync();
// Initialize Database - End

app.Run();
return;
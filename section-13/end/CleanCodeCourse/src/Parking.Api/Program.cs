using Microsoft.EntityFrameworkCore;
using Parking.Api;
using Parking.Api.ApplyPricing;
using Parking.Api.Customers;
using Parking.Api.Customers.Add;
using Parking.Api.Pricing.ApplyPricing;
using Parking.Api.TicketPrice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PricingDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("Pricing")));

builder.Services
	.AddCustomers()
	.AddPricing();

var app = builder.Build();

app.MapGet("/", () => "Parking API");
app.MapPut("/PricingTable", ApplyPricingEndpoint.HandleAsync);
app.MapGet("/TicketPrice", TicketPriceEndpoint.HandleAsync);
app.MapPost("/customers", AddCustomerEndpoint.HandleAsync);

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


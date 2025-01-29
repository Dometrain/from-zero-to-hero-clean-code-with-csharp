using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Parking.Api;

namespace Parking.Tests;

public class ParkingApplication : WebApplicationFactory<IApiAssemblyMarker>
{
    public PricingDbContext CreatePricingDbContext()
    {
        var db = Services.GetRequiredService<IDbContextFactory<PricingDbContext>>().CreateDbContext();
        db.Database.EnsureCreated();
        return db;
    }


    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.AddDbContextFactory<PricingDbContext>();

            services.RemoveAll(typeof(DbContextOptions<PricingDbContext>));
            
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<PricingDbContext>();
            dbContextOptionsBuilder.UseInMemoryDatabase(
                databaseName: "pricing");
            services.AddSingleton(dbContextOptionsBuilder.Options);

            services.AddSingleton<DbContextOptions>(s => s.GetRequiredService<DbContextOptions<PricingDbContext>>());
        });
        return base.CreateHost(builder);
    }
}
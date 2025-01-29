using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Parking.Api.Customers;
using Parking.Api.Domain;
using Parking.Api.Customers.Add;

namespace Parking.Api;

public class PricingDbContext : DbContext
{
    public PricingDbContext(DbContextOptions<PricingDbContext> options) : base(options)
    {
    }

    public DbSet<PricingTable> PricingTables => Set<PricingTable>();
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Customer>()
            .HasKey(p => p.Id);
        
        builder.Entity<Customer>()
            .Property(e => e.Address)
            .HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<Address>(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

        builder.Entity<Customer>()
            .Property(e => e.VehicleLicensePlates)
            .HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<IReadOnlyCollection<LicensePlate>>(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));


        
        builder.Entity<PricingTable>()
            .HasKey(p => p.Id);

        builder.Entity<PricingTable>()
            .Property(e => e.Tiers)
            .HasConversion(
                v => JsonConvert.SerializeObject(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<IReadOnlyCollection<PriceTier>>(v,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

        
        base.OnModelCreating(builder);
    }
}
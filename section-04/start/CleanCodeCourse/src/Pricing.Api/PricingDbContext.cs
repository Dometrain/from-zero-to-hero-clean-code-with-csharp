using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pricing.Api.AddCustomer;
using Pricing.Api.Domain;

namespace Pricing.Api;

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
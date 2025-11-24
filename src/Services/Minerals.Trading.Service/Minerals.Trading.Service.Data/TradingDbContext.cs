using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Data;

public class TradingDbContext : DbContext
{
    public TradingDbContext(DbContextOptions<TradingDbContext> options) : base(options)
    {
    }

    public DbSet<Trade> Trades { get; set; } = null!;
    public DbSet<MineralListing> MineralListings { get; set; } = null!;
    public DbSet<Buyer> Buyers { get; set; } = null!;
    public DbSet<Seller> Sellers { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    public DbSet<Warrant> Warrants { get; set; } = null!;
    public DbSet<Settlement> Settlements { get; set; } = null!;
    public DbSet<Margin> Margins { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Inspection> Inspections { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for financial fields
        modelBuilder.Entity<Trade>()
            .Property(t => t.Quantity)
            .HasPrecision(18, 4);
        
        modelBuilder.Entity<Trade>()
            .Property(t => t.PricePerTon)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<Trade>()
            .Property(t => t.TotalValue)
            .HasPrecision(18, 2);

        modelBuilder.Entity<MineralListing>()
            .Property(m => m.QuantityAvailable)
            .HasPrecision(18, 4);
        
        modelBuilder.Entity<MineralListing>()
            .Property(m => m.PricePerTon)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Settlement>()
            .Property(s => s.SettlementAmount)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<Settlement>()
            .Property(s => s.FinalPrice)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<Settlement>()
            .Property(s => s.PriceDifference)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Margin>()
            .Property(m => m.InitialMargin)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<Margin>()
            .Property(m => m.VariationMargin)
            .HasPrecision(18, 2);
        
        modelBuilder.Entity<Margin>()
            .Property(m => m.TotalMargin)
            .HasPrecision(18, 2);
    }
}

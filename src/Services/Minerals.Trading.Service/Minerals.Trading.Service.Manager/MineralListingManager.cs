using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class MineralListingManager : IMineralListingManager
{
    private readonly TradingDbContext _context;
    
    // Market price ranges for validation (per metric ton in USD)
    private static readonly Dictionary<MetalType, (decimal Min, decimal Max)> MarketPriceRanges = new()
    {
        { MetalType.Copper, (8000m, 12000m) },
        { MetalType.Aluminum, (2000m, 4000m) },
        { MetalType.Zinc, (2500m, 4500m) },
        { MetalType.Nickel, (15000m, 30000m) },
        { MetalType.Lead, (1800m, 3000m) },
        { MetalType.Tin, (25000m, 40000m) },
        { MetalType.Gold, (55000000m, 65000000m) }, // Gold is per ton, much higher
        { MetalType.Cobalt, (30000m, 50000m) }
    };

    public MineralListingManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MineralListing>> GetAllMineralListingsAsync()
    {
        return await _context.MineralListings.ToListAsync();
    }

    public async Task<MineralListing?> GetMineralListingByIdAsync(string id)
    {
        return await _context.MineralListings.FindAsync(id);
    }

    public async Task<MineralListing> CreateMineralListingAsync(MineralListing listing)
    {
        // Validate listing
        if (!await ValidateListingAsync(listing))
        {
            throw new InvalidOperationException("Listing validation failed");
        }

        listing.Id = Guid.NewGuid().ToString();
        listing.ListingDate = DateTime.Now;
        
        // Set default expiry if not provided (30 days from now)
        if (!listing.ExpiryDate.HasValue)
        {
            listing.ExpiryDate = DateTime.Now.AddDays(30);
        }
        
        _context.MineralListings.Add(listing);
        await _context.SaveChangesAsync();
        return listing;
    }

    public async Task<MineralListing> UpdateMineralListingAsync(MineralListing listing)
    {
        var existing = await _context.MineralListings.FindAsync(listing.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Mineral listing with ID {listing.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(listing);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteMineralListingAsync(string id)
    {
        var listing = await _context.MineralListings.FindAsync(id);
        if (listing == null)
        {
            return false;
        }

        _context.MineralListings.Remove(listing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MineralListing>> GetAvailableMineralListingsAsync()
    {
        return await _context.MineralListings
            .Where(m => m.Status == "Available")
            .ToListAsync();
    }

    public async Task<MineralListing> UpdateListingStatusAsync(string listingId, string status)
    {
        var listing = await _context.MineralListings.FindAsync(listingId);
        if (listing == null)
        {
            throw new KeyNotFoundException($"Mineral listing with ID {listingId} not found");
        }

        // Validate status transition
        var validStatuses = new[] { "Available", "Under Offer", "Sold", "Expired", "Withdrawn" };
        if (!validStatuses.Contains(status))
        {
            throw new ArgumentException($"Invalid status. Must be one of: {string.Join(", ", validStatuses)}");
        }

        listing.Status = status;
        await _context.SaveChangesAsync();
        return listing;
    }

    public async Task<bool> ValidateListingAsync(MineralListing listing)
    {
        if (string.IsNullOrWhiteSpace(listing.SellerId))
        {
            throw new ArgumentException("Seller ID is required");
        }

        if (string.IsNullOrWhiteSpace(listing.SellerCompanyName))
        {
            throw new ArgumentException("Seller company name is required");
        }

        if (listing.QuantityAvailable <= 0)
        {
            throw new ArgumentException("Quantity available must be greater than zero");
        }

        if (listing.QuantityAvailable > 10000m)
        {
            throw new ArgumentException("Quantity available cannot exceed 10,000 metric tons per listing");
        }

        if (listing.PricePerTon <= 0)
        {
            throw new ArgumentException("Price per ton must be greater than zero");
        }

        // Check price range
        if (!await CheckPriceRangeAsync(listing.MetalType, listing.PricePerTon))
        {
            var range = MarketPriceRanges[listing.MetalType];
            throw new InvalidOperationException(
                $"Price ${listing.PricePerTon:F2} is outside acceptable market range for {listing.MetalType} " +
                $"(${range.Min:F2} - ${range.Max:F2}). Please verify pricing.");
        }

        if (string.IsNullOrWhiteSpace(listing.OriginCountry))
        {
            throw new ArgumentException("Origin country is required");
        }

        if (string.IsNullOrWhiteSpace(listing.QualityGrade))
        {
            throw new ArgumentException("Quality grade is required");
        }

        // Check if seller is approved
        var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.Id == listing.SellerId);
        if (seller != null && !seller.IsApproved)
        {
            throw new InvalidOperationException($"Seller '{listing.SellerCompanyName}' is not approved for listing minerals");
        }

        return true;
    }

    public async Task<IEnumerable<MineralListing>> GetListingsByMetalTypeAsync(MetalType metalType)
    {
        return await _context.MineralListings
            .Where(m => m.MetalType == metalType && m.Status == "Available")
            .OrderBy(m => m.PricePerTon)
            .ToListAsync();
    }

    public Task<bool> CheckPriceRangeAsync(MetalType metalType, decimal pricePerTon)
    {
        if (!MarketPriceRanges.ContainsKey(metalType))
        {
            // If no range defined, accept any positive price
            return Task.FromResult(pricePerTon > 0);
        }

        var (min, max) = MarketPriceRanges[metalType];
        
        // Allow 20% deviation outside the range for flexibility
        var minWithBuffer = min * 0.8m;
        var maxWithBuffer = max * 1.2m;
        
        return Task.FromResult(pricePerTon >= minWithBuffer && pricePerTon <= maxWithBuffer);
    }
}

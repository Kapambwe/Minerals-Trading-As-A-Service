using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class MineralListingManager : IMineralListingManager
{
    private readonly TradingDbContext _context;

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
        listing.Id = Guid.NewGuid().ToString();
        listing.ListingDate = DateTime.Now;
        
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

        listing.Status = status;
        await _context.SaveChangesAsync();
        return listing;
    }
}

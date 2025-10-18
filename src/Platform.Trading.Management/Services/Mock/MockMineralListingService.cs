using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockMineralListingService : IMineralListingService
{
    private readonly List<MineralListing> _listings;

    public MockMineralListingService()
    {
        _listings = new List<MineralListing>
        {
            new MineralListing
            {
                Id = "ML001",
                SellerId = "SEL001",
                SellerCompanyName = "Konkola Copper Mines PLC",
                MetalType = MetalType.Copper,
                QuantityAvailable = 1000,
                PricePerTon = 8800,
                OriginCountry = "Zambia",
                QualityGrade = "Grade A Cathodes",
                ListingDate = DateTime.Now.AddDays(-10),
                ExpiryDate = DateTime.Now.AddDays(20),
                Status = "Available",
                Notes = "High purity copper cathodes, ready for immediate shipment."
            },
            new MineralListing
            {
                Id = "ML002",
                SellerId = "SEL004",
                SellerCompanyName = "Kansanshi Mining PLC",
                MetalType = MetalType.Gold,
                QuantityAvailable = 50, // in kg, assuming 1 MT = 1000 kg for consistency, but gold is usually in kg/oz
                PricePerTon = 65000000, // Price per kg, assuming 1 MT = 1000 kg
                OriginCountry = "Zambia",
                QualityGrade = "99.99% Pure Gold",
                ListingDate = DateTime.Now.AddDays(-5),
                ExpiryDate = DateTime.Now.AddDays(15),
                Status = "Available",
                Notes = "Investment grade gold bars."
            },
            new MineralListing
            {
                Id = "ML003",
                SellerId = "SEL001",
                SellerCompanyName = "Konkola Copper Mines PLC",
                MetalType = MetalType.Cobalt,
                QuantityAvailable = 200,
                PricePerTon = 30000,
                OriginCountry = "Zambia",
                QualityGrade = "Cobalt Hydroxide",
                ListingDate = DateTime.Now.AddDays(-7),
                ExpiryDate = DateTime.Now.AddDays(25),
                Status = "Available",
                Notes = "High-grade cobalt hydroxide, suitable for battery production."
            },
            new MineralListing
            {
                Id = "ML004",
                SellerId = "SEL003",
                SellerCompanyName = "Mufumbwe Small Scale Miners Cooperative",
                MetalType = MetalType.Copper,
                QuantityAvailable = 50,
                PricePerTon = 8750,
                OriginCountry = "Zambia",
                QualityGrade = "Copper Concentrates",
                ListingDate = DateTime.Now.AddDays(-3),
                ExpiryDate = DateTime.Now.AddDays(10),
                Status = "Available",
                Notes = "Small batch copper concentrates, direct from cooperative."
            }
        };
    }

    public Task<IEnumerable<MineralListing>> GetAllMineralListingsAsync()
    {
        return Task.FromResult<IEnumerable<MineralListing>>(_listings);
    }

    public Task<MineralListing?> GetMineralListingByIdAsync(string id)
    {
        var listing = _listings.FirstOrDefault(l => l.Id == id);
        return Task.FromResult(listing);
    }

    public Task<MineralListing> CreateMineralListingAsync(MineralListing listing)
    {
        listing.Id = $"ML{_listings.Count + 1:D3}";
        listing.ListingDate = DateTime.Now;
        listing.Status = "Available";
        _listings.Add(listing);
        return Task.FromResult(listing);
    }

    public Task<MineralListing> UpdateMineralListingAsync(MineralListing listing)
    {
        var existingListing = _listings.FirstOrDefault(l => l.Id == listing.Id);
        if (existingListing != null)
        {
            var index = _listings.IndexOf(existingListing);
            _listings[index] = listing;
        }
        return Task.FromResult(listing);
    }

    public Task<bool> DeleteMineralListingAsync(string id)
    {
        var listing = _listings.FirstOrDefault(l => l.Id == id);
        if (listing != null)
        {
            _listings.Remove(listing);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<IEnumerable<MineralListing>> GetAvailableMineralListingsAsync()
    {
        var availableListings = _listings.Where(l => l.Status == "Available" && (!l.ExpiryDate.HasValue || l.ExpiryDate.Value > DateTime.Now));
        return Task.FromResult<IEnumerable<MineralListing>>(availableListings);
    }

    public Task<MineralListing> UpdateListingStatusAsync(string listingId, string status)
    {
        var listing = _listings.FirstOrDefault(l => l.Id == listingId);
        if (listing != null)
        {
            listing.Status = status;
        }
        return Task.FromResult(listing!); // Return the updated listing
    }
}

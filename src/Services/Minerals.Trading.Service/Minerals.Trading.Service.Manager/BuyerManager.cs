using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class BuyerManager : IBuyerManager
{
    private readonly TradingDbContext _context;

    public BuyerManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Buyer>> GetAllBuyersAsync()
    {
        return await _context.Buyers.ToListAsync();
    }

    public async Task<Buyer?> GetBuyerByIdAsync(string id)
    {
        return await _context.Buyers.FindAsync(id);
    }

    public async Task<Buyer> CreateBuyerAsync(Buyer buyer)
    {
        buyer.Id = Guid.NewGuid().ToString();
        buyer.RegistrationDate = DateTime.Now;
        buyer.LastKYCReviewDate = DateTime.Now;
        
        _context.Buyers.Add(buyer);
        await _context.SaveChangesAsync();
        return buyer;
    }

    public async Task<Buyer> UpdateBuyerAsync(Buyer buyer)
    {
        var existing = await _context.Buyers.FindAsync(buyer.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Buyer with ID {buyer.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(buyer);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteBuyerAsync(string id)
    {
        var buyer = await _context.Buyers.FindAsync(id);
        if (buyer == null)
        {
            return false;
        }

        _context.Buyers.Remove(buyer);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Buyer> ApproveBuyerAsync(string buyerId)
    {
        var buyer = await _context.Buyers.FindAsync(buyerId);
        if (buyer == null)
        {
            throw new KeyNotFoundException($"Buyer with ID {buyerId} not found");
        }

        buyer.IsApproved = true;
        buyer.Status = "KYC Approved";
        buyer.KYCStatus = "Approved";
        await _context.SaveChangesAsync();
        return buyer;
    }

    public async Task<Buyer> UpdateKYCStatusAsync(string buyerId, string kycStatus)
    {
        var buyer = await _context.Buyers.FindAsync(buyerId);
        if (buyer == null)
        {
            throw new KeyNotFoundException($"Buyer with ID {buyerId} not found");
        }

        buyer.KYCStatus = kycStatus;
        buyer.LastKYCReviewDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return buyer;
    }
}

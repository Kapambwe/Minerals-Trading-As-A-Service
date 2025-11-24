using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class SellerManager : ISellerManager
{
    private readonly TradingDbContext _context;

    public SellerManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Seller>> GetAllSellersAsync()
    {
        return await _context.Sellers.ToListAsync();
    }

    public async Task<Seller?> GetSellerByIdAsync(string id)
    {
        return await _context.Sellers.FindAsync(id);
    }

    public async Task<Seller> CreateSellerAsync(Seller seller)
    {
        seller.Id = Guid.NewGuid().ToString();
        seller.RegistrationDate = DateTime.Now;
        seller.LastKYCReviewDate = DateTime.Now;
        
        _context.Sellers.Add(seller);
        await _context.SaveChangesAsync();
        return seller;
    }

    public async Task<Seller> UpdateSellerAsync(Seller seller)
    {
        var existing = await _context.Sellers.FindAsync(seller.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Seller with ID {seller.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(seller);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteSellerAsync(string id)
    {
        var seller = await _context.Sellers.FindAsync(id);
        if (seller == null)
        {
            return false;
        }

        _context.Sellers.Remove(seller);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Seller> ApproveSellerAsync(string sellerId)
    {
        var seller = await _context.Sellers.FindAsync(sellerId);
        if (seller == null)
        {
            throw new KeyNotFoundException($"Seller with ID {sellerId} not found");
        }

        seller.IsApproved = true;
        seller.Status = "KYC Approved";
        seller.KYCStatus = "Approved";
        await _context.SaveChangesAsync();
        return seller;
    }

    public async Task<Seller> UpdateKYCStatusAsync(string sellerId, string kycStatus)
    {
        var seller = await _context.Sellers.FindAsync(sellerId);
        if (seller == null)
        {
            throw new KeyNotFoundException($"Seller with ID {sellerId} not found");
        }

        seller.KYCStatus = kycStatus;
        seller.LastKYCReviewDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return seller;
    }
}

using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class WarrantManager : IWarrantManager
{
    private readonly TradingDbContext _context;

    public WarrantManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Warrant>> GetAllWarrantsAsync()
    {
        return await _context.Warrants.ToListAsync();
    }

    public async Task<Warrant?> GetWarrantByIdAsync(string id)
    {
        return await _context.Warrants.FindAsync(id);
    }

    public async Task<IEnumerable<Warrant>> GetWarrantsByTradeIdAsync(string tradeId)
    {
        return await _context.Warrants
            .Where(w => w.TradeId == tradeId)
            .ToListAsync();
    }

    public async Task<Warrant> CreateWarrantAsync(Warrant warrant)
    {
        // Validate warrant
        if (!await ValidateWarrantAsync(warrant))
        {
            throw new InvalidOperationException("Warrant validation failed");
        }

        warrant.Id = Guid.NewGuid().ToString();
        warrant.IssueDate = DateTime.Now;
        
        if (string.IsNullOrEmpty(warrant.WarrantNumber))
        {
            warrant.WarrantNumber = $"WRN-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
        
        _context.Warrants.Add(warrant);
        await _context.SaveChangesAsync();
        return warrant;
    }

    public async Task<Warrant> UpdateWarrantAsync(Warrant warrant)
    {
        var existing = await _context.Warrants.FindAsync(warrant.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Warrant with ID {warrant.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(warrant);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteWarrantAsync(string id)
    {
        var warrant = await _context.Warrants.FindAsync(id);
        if (warrant == null)
        {
            return false;
        }

        _context.Warrants.Remove(warrant);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Warrant> TransferWarrantAsync(string warrantId, string newOwner)
    {
        var warrant = await _context.Warrants.FindAsync(warrantId);
        if (warrant == null)
        {
            throw new KeyNotFoundException($"Warrant with ID {warrantId} not found");
        }

        if (!warrant.IsActive)
        {
            throw new InvalidOperationException("Cannot transfer an inactive warrant");
        }

        if (string.IsNullOrWhiteSpace(newOwner))
        {
            throw new ArgumentException("New owner name is required");
        }

        if (warrant.CurrentOwner.Equals(newOwner, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("New owner cannot be the same as current owner");
        }

        warrant.PreviousOwner = warrant.CurrentOwner;
        warrant.CurrentOwner = newOwner;
        warrant.TransferDate = DateTime.Now;
        warrant.Notes = string.IsNullOrEmpty(warrant.Notes)
            ? $"Transferred to {newOwner} on {DateTime.Now:yyyy-MM-dd}"
            : $"{warrant.Notes}\nTransferred to {newOwner} on {DateTime.Now:yyyy-MM-dd}";
        
        await _context.SaveChangesAsync();
        return warrant;
    }

    public async Task<bool> ValidateWarrantAsync(Warrant warrant)
    {
        if (string.IsNullOrWhiteSpace(warrant.WarehouseId))
        {
            throw new ArgumentException("Warehouse ID is required");
        }

        // Verify warehouse exists and is approved
        var warehouse = await _context.Warehouses.FindAsync(warrant.WarehouseId);
        if (warehouse == null)
        {
            throw new KeyNotFoundException($"Warehouse with ID {warrant.WarehouseId} not found");
        }

        if (!warehouse.IsLMEApproved)
        {
            throw new InvalidOperationException($"Warehouse '{warehouse.OperatorName}' at {warehouse.Location} is not LME approved");
        }

        if (warrant.Quantity <= 0)
        {
            throw new ArgumentException("Warrant quantity must be greater than zero");
        }

        // Check warehouse capacity
        if (!await VerifyWarehouseCapacityAsync(warrant.WarehouseId, warrant.Quantity))
        {
            throw new InvalidOperationException($"Warehouse does not have sufficient capacity for {warrant.Quantity} metric tons");
        }

        if (string.IsNullOrWhiteSpace(warrant.CurrentOwner))
        {
            throw new ArgumentException("Current owner is required");
        }

        if (string.IsNullOrWhiteSpace(warrant.QualityGrade))
        {
            throw new ArgumentException("Quality grade is required");
        }

        if (string.IsNullOrWhiteSpace(warrant.LotNumber))
        {
            throw new ArgumentException("Lot number is required");
        }

        return true;
    }

    public async Task<bool> VerifyWarehouseCapacityAsync(string warehouseId, decimal additionalQuantity)
    {
        var warehouse = await _context.Warehouses.FindAsync(warehouseId);
        if (warehouse == null)
        {
            return false;
        }

        // Calculate current stored quantity
        var currentStored = await _context.Warrants
            .Where(w => w.WarehouseId == warehouseId && w.IsActive)
            .SumAsync(w => w.Quantity);

        var totalAfterAddition = currentStored + additionalQuantity;

        // Check if total would exceed capacity
        return totalAfterAddition <= warehouse.StorageCapacity;
    }
}

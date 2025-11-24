using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class WarehouseManager : IWarehouseManager
{
    private readonly TradingDbContext _context;

    public WarehouseManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        return await _context.Warehouses.ToListAsync();
    }

    public async Task<Warehouse?> GetWarehouseByIdAsync(string id)
    {
        return await _context.Warehouses.FindAsync(id);
    }

    public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
    {
        warehouse.Id = Guid.NewGuid().ToString();
        
        _context.Warehouses.Add(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }

    public async Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse)
    {
        var existing = await _context.Warehouses.FindAsync(warehouse.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Warehouse with ID {warehouse.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(warehouse);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteWarehouseAsync(string id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null)
        {
            return false;
        }

        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Warehouse>> GetApprovedWarehousesAsync()
    {
        return await _context.Warehouses
            .Where(w => w.IsLMEApproved)
            .ToListAsync();
    }

    public async Task<Warehouse> ApproveWarehouseAsync(string warehouseId)
    {
        var warehouse = await _context.Warehouses.FindAsync(warehouseId);
        if (warehouse == null)
        {
            throw new KeyNotFoundException($"Warehouse with ID {warehouseId} not found");
        }

        warehouse.IsLMEApproved = true;
        warehouse.ApprovalDate = DateTime.Now;
        await _context.SaveChangesAsync();
        return warehouse;
    }
}

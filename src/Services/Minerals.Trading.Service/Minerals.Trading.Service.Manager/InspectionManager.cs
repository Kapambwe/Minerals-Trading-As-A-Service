using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class InspectionManager : IInspectionManager
{
    private readonly TradingDbContext _context;

    public InspectionManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inspection>> GetAllInspectionsAsync()
    {
        return await _context.Inspections.ToListAsync();
    }

    public async Task<Inspection?> GetInspectionByIdAsync(string id)
    {
        return await _context.Inspections.FindAsync(id);
    }

    public async Task<IEnumerable<Inspection>> GetInspectionsByWarehouseIdAsync(string warehouseId)
    {
        return await _context.Inspections
            .Where(i => i.WarehouseId == warehouseId)
            .ToListAsync();
    }

    public async Task<Inspection> CreateInspectionAsync(Inspection inspection)
    {
        inspection.Id = Guid.NewGuid().ToString();
        inspection.InspectionDate = DateTime.Now;
        
        _context.Inspections.Add(inspection);
        await _context.SaveChangesAsync();
        return inspection;
    }

    public async Task<Inspection> UpdateInspectionAsync(Inspection inspection)
    {
        var existing = await _context.Inspections.FindAsync(inspection.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Inspection with ID {inspection.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(inspection);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteInspectionAsync(string id)
    {
        var inspection = await _context.Inspections.FindAsync(id);
        if (inspection == null)
        {
            return false;
        }

        _context.Inspections.Remove(inspection);
        await _context.SaveChangesAsync();
        return true;
    }
}

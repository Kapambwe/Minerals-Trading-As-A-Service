using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface IWarehouseService
{
    Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
    Task<Warehouse?> GetWarehouseByIdAsync(string id);
    Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse);
    Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse);
    Task<bool> DeleteWarehouseAsync(string id);
    Task<IEnumerable<Warehouse>> GetApprovedWarehousesAsync();
    Task<Warehouse> ApproveWarehouseAsync(string warehouseId);
}

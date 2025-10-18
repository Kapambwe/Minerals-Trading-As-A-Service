using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface IInspectionService
{
    Task<IEnumerable<Inspection>> GetAllInspectionsAsync();
    Task<Inspection?> GetInspectionByIdAsync(string id);
    Task<Inspection> CreateInspectionAsync(Inspection inspection);
    Task<Inspection> UpdateInspectionAsync(Inspection inspection);
    Task<bool> DeleteInspectionAsync(string id);
    Task<IEnumerable<Inspection>> GetInspectionsByWarehouseIdAsync(string warehouseId);
}

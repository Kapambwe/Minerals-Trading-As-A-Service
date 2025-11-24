using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface IInspectionManager
{
    Task<IEnumerable<Inspection>> GetAllInspectionsAsync();
    Task<Inspection?> GetInspectionByIdAsync(string id);
    Task<IEnumerable<Inspection>> GetInspectionsByWarehouseIdAsync(string warehouseId);
    Task<Inspection> CreateInspectionAsync(Inspection inspection);
    Task<Inspection> UpdateInspectionAsync(Inspection inspection);
    Task<bool> DeleteInspectionAsync(string id);
}

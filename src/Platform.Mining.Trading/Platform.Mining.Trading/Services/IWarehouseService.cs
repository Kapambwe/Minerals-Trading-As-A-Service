using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IWarehouseService
    {
        Task<List<WarehouseLocation>> GetWarehouseLocationsAsync();
        Task<List<WarehouseReceipt>> GetWarehouseReceiptsAsync();
        Task<string> CreateReceiptAsync(WarehouseReceipt receipt);
        Task<bool> AttachQcCertificateAsync(string receiptId, QualityCertificate certificate);
        Task<bool> MarkDeliveryCompleteAsync(string receiptId);
        Task<List<InventorySummary>> GetInventorySummaryAsync();
        Task<List<QualityCertificate>> GetQualityCertificatesAsync();
    }
}

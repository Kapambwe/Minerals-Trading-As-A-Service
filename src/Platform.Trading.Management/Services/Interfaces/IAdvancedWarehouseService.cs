using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.WarehouseManagement;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Interface for electronic warehouse receipt service.
/// Addresses Gap WD-001: Electronic Warehouse Receipt (EWR) system with legal backing.
/// </summary>
public interface IWarehouseReceiptService
{
    Task<ElectronicWarehouseReceipt?> GetReceiptAsync(string receiptNumber);
    Task<List<ElectronicWarehouseReceipt>> GetAllReceiptsAsync();
    Task<List<ElectronicWarehouseReceipt>> GetReceiptsByOwnerAsync(string ownerId);
    Task<ElectronicWarehouseReceipt> IssueReceiptAsync(ElectronicWarehouseReceipt receipt);
    Task<ElectronicWarehouseReceipt> TransferOwnershipAsync(string receiptNumber, string toOwnerId, string toOwnerName, string transferType);
    Task<ElectronicWarehouseReceipt> PledgeReceiptAsync(string receiptNumber, string pledgeeId, string pledgeType);
    Task<ElectronicWarehouseReceipt> ReleaseEncumbranceAsync(string receiptNumber);
    Task<ElectronicWarehouseReceipt> RedeemReceiptAsync(string receiptNumber);
    Task<ElectronicWarehouseReceipt> CancelReceiptAsync(string receiptNumber, string reason);
    Task<bool> VerifySignatureAsync(string receiptNumber);
    Task RegisterWithRegulatorAsync(string receiptNumber);
}

/// <summary>
/// Interface for warehouse insurance service.
/// Addresses Gap WD-007: Automated insurance for stored metals.
/// </summary>
public interface IWarehouseInsuranceService
{
    Task<WarehouseInsurance?> GetInsuranceAsync(string policyNumber);
    Task<List<WarehouseInsurance>> GetAllInsuranceAsync();
    Task<WarehouseInsurance?> GetInsuranceByWarehouseAsync(string warehouseId);
    Task<WarehouseInsurance> CreateInsuranceAsync(WarehouseInsurance insurance);
    Task<WarehouseInsurance> UpdateInsuranceAsync(WarehouseInsurance insurance);
    Task<WarehouseInsurance> RenewInsuranceAsync(string policyNumber);
    Task<InsuranceClaim> FileClaimAsync(string policyNumber, InsuranceClaim claim);
    Task<InsuranceClaim> UpdateClaimStatusAsync(string claimNumber, string status, decimal? approvedAmount = null);
    Task<List<WarehouseInsurance>> GetExpiringPoliciesAsync(int daysToExpiry);
    Task<bool> VerifyInsuranceCoverageAsync(string warehouseId, decimal valueToCover);
    Task<decimal> CalculatePremiumAsync(string warehouseId, decimal coveredValue);
}

/// <summary>
/// Interface for load-out queue service.
/// Addresses Gap WD-006: Queue management for metal withdrawal.
/// </summary>
public interface ILoadOutQueueService
{
    Task<LoadOutQueue?> GetQueueItemAsync(string queueNumber);
    Task<List<LoadOutQueue>> GetQueueByWarehouseAsync(string warehouseId);
    Task<LoadOutQueue> CreateQueueRequestAsync(LoadOutQueue request);
    Task<LoadOutQueue> UpdateQueuePositionAsync(string queueNumber, int newPosition);
    Task<LoadOutQueue> ScheduleLoadOutAsync(string queueNumber, DateTime scheduledDate, string timeSlot);
    Task<LoadOutQueue> CompleteLoadOutAsync(string queueNumber, decimal actualQuantity);
    Task<LoadOutQueue> CancelQueueRequestAsync(string queueNumber, string reason);
    Task<int> GetEstimatedWaitDaysAsync(string warehouseId);
    Task<List<LoadOutQueue>> GetScheduledLoadOutsAsync(string warehouseId, DateTime date);
}

/// <summary>
/// Interface for storage fee service.
/// Addresses Gap WD-010: Automated rent calculation and billing.
/// </summary>
public interface IStorageFeeService
{
    Task<StorageFee?> GetStorageFeeAsync(string invoiceNumber);
    Task<List<StorageFee>> GetStorageFeesByHolderAsync(string warrantHolderId);
    Task<List<StorageFee>> GenerateMonthlyInvoicesAsync(string billingPeriod);
    Task<StorageFee> CalculateStorageFeeAsync(string warehouseId, string warrantHolderId, string billingPeriod);
    Task<StorageFee> RecordPaymentAsync(string invoiceNumber, decimal amount, string reference);
    Task<List<StorageFee>> GetOverdueInvoicesAsync();
    Task<decimal> GetStorageRateAsync(string warehouseId, MetalType metalType);
    Task ApplyLateFeeAsync(string invoiceNumber);
}

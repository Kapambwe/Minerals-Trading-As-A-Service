using Platform.Trading.Management.Models.ChainOfCustody;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for chain of custody and traceability operations.
/// </summary>
public interface IChainOfCustodyService
{
    // Custody Records
    Task<IEnumerable<CustodyRecord>> GetAllCustodyRecordsAsync();
    Task<CustodyRecord?> GetCustodyRecordByIdAsync(string id);
    Task<IEnumerable<CustodyRecord>> GetCustodyChainAsync(string custodyChainId);
    Task<IEnumerable<CustodyRecord>> GetCustodyRecordsByBatchAsync(string batchNumber);
    Task<CustodyRecord> CreateCustodyRecordAsync(CustodyRecord record);
    Task<CustodyRecord> UpdateCustodyRecordAsync(CustodyRecord record);
    Task<CustodyRecord> VerifyCustodyRecordAsync(string id, string verifiedBy, decimal verifiedQuantity);
    Task<bool> DeleteCustodyRecordAsync(string id);
    
    // Assay Certificates
    Task<IEnumerable<AssayCertificate>> GetAllAssayCertificatesAsync();
    Task<AssayCertificate?> GetAssayCertificateByIdAsync(string id);
    Task<AssayCertificate?> GetAssayCertificateByCertificateNumberAsync(string certificateNumber);
    Task<IEnumerable<AssayCertificate>> GetAssayCertificatesByBatchAsync(string batchNumber);
    Task<AssayCertificate> CreateAssayCertificateAsync(AssayCertificate certificate);
    Task<AssayCertificate> UpdateAssayCertificateAsync(AssayCertificate certificate);
    Task<AssayCertificate> VerifyAssayCertificateAsync(string id);
    Task<bool> DeleteAssayCertificateAsync(string id);
    
    // Traceability
    Task<IEnumerable<CustodyRecord>> TraceBackToMineAsync(string warrantId);
    Task<bool> ValidateChainIntegrityAsync(string custodyChainId);
    Task<bool> ValidateConflictMineralsComplianceAsync(string custodyChainId);
}

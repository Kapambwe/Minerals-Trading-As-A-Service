using Platform.Trading.Management.Models.AmlKyc;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for AML/KYC screening and compliance operations.
/// </summary>
public interface IAmlKycService
{
    // AML Screening
    Task<IEnumerable<AmlScreeningResult>> GetAllScreeningResultsAsync();
    Task<AmlScreeningResult?> GetScreeningResultByIdAsync(string id);
    Task<IEnumerable<AmlScreeningResult>> GetScreeningResultsByEntityAsync(string entityId, string entityType);
    Task<AmlScreeningResult> CreateScreeningResultAsync(AmlScreeningResult result);
    Task<AmlScreeningResult> UpdateScreeningResultAsync(AmlScreeningResult result);
    Task<AmlScreeningResult> PerformScreeningAsync(string entityId, string entityType);
    
    // Beneficial Owners
    Task<IEnumerable<BeneficialOwner>> GetAllBeneficialOwnersAsync();
    Task<IEnumerable<BeneficialOwner>> GetBeneficialOwnersByEntityAsync(string entityId, string entityType);
    Task<BeneficialOwner?> GetBeneficialOwnerByIdAsync(string id);
    Task<BeneficialOwner> CreateBeneficialOwnerAsync(BeneficialOwner owner);
    Task<BeneficialOwner> UpdateBeneficialOwnerAsync(BeneficialOwner owner);
    Task<bool> DeleteBeneficialOwnerAsync(string id);
    Task<BeneficialOwner> VerifyBeneficialOwnerAsync(string id, string verifiedBy);
    
    // Suspicious Activity Reports
    Task<IEnumerable<SuspiciousActivityReport>> GetAllSarsAsync();
    Task<SuspiciousActivityReport?> GetSarByIdAsync(string id);
    Task<IEnumerable<SuspiciousActivityReport>> GetSarsByStatusAsync(string status);
    Task<SuspiciousActivityReport> CreateSarAsync(SuspiciousActivityReport report);
    Task<SuspiciousActivityReport> UpdateSarAsync(SuspiciousActivityReport report);
    Task<SuspiciousActivityReport> FileSarAsync(string id, string filedBy);
}

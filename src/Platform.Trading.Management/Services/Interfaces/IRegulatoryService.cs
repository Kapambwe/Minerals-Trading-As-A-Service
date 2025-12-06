using Platform.Trading.Management.Models.Regulatory;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Interface for mining license verification service.
/// Addresses Gap RC-004: Automated verification against Zambia Mining Cadastre.
/// </summary>
public interface IMiningLicenseService
{
    Task<MiningLicenseVerification?> GetVerificationAsync(string licenseNumber);
    Task<List<MiningLicenseVerification>> GetAllVerificationsAsync();
    Task<MiningLicenseVerification> VerifyLicenseAsync(string licenseNumber);
    Task<MiningLicenseVerification> CreateVerificationAsync(MiningLicenseVerification verification);
    Task<MiningLicenseVerification> UpdateVerificationAsync(MiningLicenseVerification verification);
    Task<bool> IsLicenseValidAsync(string licenseNumber);
    Task<List<MiningLicenseVerification>> GetExpiringLicensesAsync(int daysToExpiry);
    Task ScheduleAutomatedVerificationAsync(string licenseNumber, int frequencyDays);
}

/// <summary>
/// Interface for ZCCM-IH integration service.
/// Addresses Gap RC-005: Interface with national mining investment authority.
/// </summary>
public interface IZccmIntegrationService
{
    Task<ZccmIntegration?> GetIntegrationAsync(string id);
    Task<List<ZccmIntegration>> GetAllIntegrationsAsync();
    Task<ZccmIntegration> CreateIntegrationAsync(ZccmIntegration integration);
    Task<ZccmIntegration> UpdateIntegrationAsync(ZccmIntegration integration);
    Task<List<ZccmProductionRecord>> GetProductionRecordsAsync(string entityId, DateTime fromDate, DateTime toDate);
    Task<ZccmProductionRecord> ReportProductionAsync(ZccmProductionRecord record);
    Task SyncWithZccmAsync(string entityId);
    Task<decimal> CalculateRoyaltiesAsync(string entityId, DateTime period);
}

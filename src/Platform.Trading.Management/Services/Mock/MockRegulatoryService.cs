using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Regulatory;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of mining license verification service.
/// </summary>
public class MockMiningLicenseService : IMiningLicenseService
{
    private readonly List<MiningLicenseVerification> _verifications = new();

    public MockMiningLicenseService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _verifications.AddRange(new[]
        {
            new MiningLicenseVerification
            {
                Id = Guid.NewGuid().ToString(),
                LicenseNumber = "LML-2024-001",
                LicenseType = "LargeMining",
                LicenseHolderName = "Konkola Copper Mines PLC",
                LicenseHolderId = "KCM-001",
                LicenseIssueDate = new DateTime(2020, 1, 15),
                LicenseExpiryDate = new DateTime(2045, 1, 14),
                LicenseStatus = "Valid",
                AreaSize = 25000,
                Province = "Copperbelt",
                District = "Chingola",
                AuthorizedMinerals = new List<MetalType> { MetalType.Copper, MetalType.Cobalt },
                VerificationStatus = "Verified",
                IsVerified = true,
                CadastreReference = "CAD-2024-001",
                EnvironmentalClearanceValid = true,
                TaxClearanceValid = true,
                SafetyCertificateValid = true,
                AutomatedVerification = true,
                NextVerificationDate = DateTime.UtcNow.AddDays(30)
            },
            new MiningLicenseVerification
            {
                Id = Guid.NewGuid().ToString(),
                LicenseNumber = "SML-2024-102",
                LicenseType = "SmallScale",
                LicenseHolderName = "Lusaka Mining Cooperative",
                LicenseHolderId = "LMC-102",
                LicenseIssueDate = new DateTime(2023, 6, 1),
                LicenseExpiryDate = new DateTime(2028, 5, 31),
                LicenseStatus = "Valid",
                AreaSize = 500,
                Province = "Lusaka",
                District = "Chongwe",
                AuthorizedMinerals = new List<MetalType> { MetalType.Manganese },
                VerificationStatus = "Verified",
                IsVerified = true,
                AutomatedVerification = true
            }
        });
    }

    public Task<MiningLicenseVerification?> GetVerificationAsync(string licenseNumber)
        => Task.FromResult(_verifications.FirstOrDefault(v => v.LicenseNumber == licenseNumber));

    public Task<List<MiningLicenseVerification>> GetAllVerificationsAsync()
        => Task.FromResult(_verifications.ToList());

    public Task<MiningLicenseVerification> VerifyLicenseAsync(string licenseNumber)
    {
        var verification = _verifications.FirstOrDefault(v => v.LicenseNumber == licenseNumber);
        if (verification != null)
        {
            verification.VerificationDate = DateTime.UtcNow;
            verification.VerificationStatus = "Verified";
            verification.IsVerified = true;
            verification.NextVerificationDate = DateTime.UtcNow.AddDays(verification.VerificationFrequencyDays);
        }
        return Task.FromResult(verification ?? throw new InvalidOperationException($"License {licenseNumber} not found"));
    }

    public Task<MiningLicenseVerification> CreateVerificationAsync(MiningLicenseVerification verification)
    {
        verification.Id = Guid.NewGuid().ToString();
        verification.VerificationDate = DateTime.UtcNow;
        _verifications.Add(verification);
        return Task.FromResult(verification);
    }

    public Task<MiningLicenseVerification> UpdateVerificationAsync(MiningLicenseVerification verification)
    {
        var existing = _verifications.FindIndex(v => v.Id == verification.Id);
        if (existing >= 0)
            _verifications[existing] = verification;
        return Task.FromResult(verification);
    }

    public Task<bool> IsLicenseValidAsync(string licenseNumber)
    {
        var verification = _verifications.FirstOrDefault(v => v.LicenseNumber == licenseNumber);
        return Task.FromResult(verification?.IsVerified == true && verification?.LicenseStatus == "Valid");
    }

    public Task<List<MiningLicenseVerification>> GetExpiringLicensesAsync(int daysToExpiry)
    {
        var expiryDate = DateTime.UtcNow.AddDays(daysToExpiry);
        return Task.FromResult(_verifications
            .Where(v => v.LicenseExpiryDate.HasValue && v.LicenseExpiryDate.Value <= expiryDate)
            .ToList());
    }

    public Task ScheduleAutomatedVerificationAsync(string licenseNumber, int frequencyDays)
    {
        var verification = _verifications.FirstOrDefault(v => v.LicenseNumber == licenseNumber);
        if (verification != null)
        {
            verification.AutomatedVerification = true;
            verification.VerificationFrequencyDays = frequencyDays;
            verification.NextVerificationDate = DateTime.UtcNow.AddDays(frequencyDays);
        }
        return Task.CompletedTask;
    }
}

/// <summary>
/// Mock implementation of ZCCM-IH integration service.
/// </summary>
public class MockZccmIntegrationService : IZccmIntegrationService
{
    private readonly List<ZccmIntegration> _integrations = new();

    public MockZccmIntegrationService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _integrations.Add(new ZccmIntegration
        {
            Id = Guid.NewGuid().ToString(),
            ZccmEntityId = "ZCCM-KCM-001",
            EntityType = "Mine",
            EntityName = "Konkola Copper Mines - Nchanga Division",
            ZccmOwnershipPercentage = 20.6m,
            OperatingPartner = "Vedanta Resources",
            OperatingPartnerPercentage = 79.4m,
            CurrentMonthProduction = 45000,
            YearToDateProduction = 180000,
            RoyaltiesPayable = 2500000,
            ComplianceStatus = "Compliant",
            IntegrationStatus = "Active",
            LastSyncDate = DateTime.UtcNow.AddHours(-2),
            SyncStatus = "Synced"
        });
    }

    public Task<ZccmIntegration?> GetIntegrationAsync(string id)
        => Task.FromResult(_integrations.FirstOrDefault(i => i.Id == id));

    public Task<List<ZccmIntegration>> GetAllIntegrationsAsync()
        => Task.FromResult(_integrations.ToList());

    public Task<ZccmIntegration> CreateIntegrationAsync(ZccmIntegration integration)
    {
        integration.Id = Guid.NewGuid().ToString();
        integration.IntegrationDate = DateTime.UtcNow;
        _integrations.Add(integration);
        return Task.FromResult(integration);
    }

    public Task<ZccmIntegration> UpdateIntegrationAsync(ZccmIntegration integration)
    {
        var existing = _integrations.FindIndex(i => i.Id == integration.Id);
        if (existing >= 0)
            _integrations[existing] = integration;
        return Task.FromResult(integration);
    }

    public Task<List<ZccmProductionRecord>> GetProductionRecordsAsync(string entityId, DateTime fromDate, DateTime toDate)
    {
        var integration = _integrations.FirstOrDefault(i => i.ZccmEntityId == entityId);
        return Task.FromResult(integration?.ProductionRecords
            .Where(r => r.RecordDate >= fromDate && r.RecordDate <= toDate)
            .ToList() ?? new List<ZccmProductionRecord>());
    }

    public Task<ZccmProductionRecord> ReportProductionAsync(ZccmProductionRecord record)
    {
        record.Id = Guid.NewGuid().ToString();
        return Task.FromResult(record);
    }

    public Task SyncWithZccmAsync(string entityId)
    {
        var integration = _integrations.FirstOrDefault(i => i.ZccmEntityId == entityId);
        if (integration != null)
        {
            integration.LastSyncDate = DateTime.UtcNow;
            integration.SyncStatus = "Synced";
        }
        return Task.CompletedTask;
    }

    public Task<decimal> CalculateRoyaltiesAsync(string entityId, DateTime period)
    {
        // Mock royalty calculation (8% of production value)
        return Task.FromResult(250000m);
    }
}

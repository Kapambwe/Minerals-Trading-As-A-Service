using Platform.Trading.Management.Models.AmlKyc;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IAmlKycService for development and testing.
/// </summary>
public class MockAmlKycService : IAmlKycService
{
    private readonly List<AmlScreeningResult> _screeningResults = new();
    private readonly List<BeneficialOwner> _beneficialOwners = new();
    private readonly List<SuspiciousActivityReport> _sars = new();

    public MockAmlKycService()
    {
        SeedData();
    }

    private void SeedData()
    {
        _screeningResults.AddRange(new[]
        {
            new AmlScreeningResult
            {
                EntityId = "buyer-001",
                EntityType = "Buyer",
                EntityName = "Copper Trading Ltd",
                SanctionsCheckPassed = true,
                PepCheckPassed = true,
                AdverseMediaCheckPassed = true,
                RiskScore = 25,
                RiskLevel = "Low",
                ScreeningStatus = "Passed",
                SanctionsListsChecked = "OFAC, EU, UN, UK",
                ExpiryDate = DateTime.Now.AddYears(1),
                NextReviewDate = DateTime.Now.AddMonths(6)
            },
            new AmlScreeningResult
            {
                EntityId = "seller-001",
                EntityType = "Seller",
                EntityName = "Zambia Mining Corp",
                SanctionsCheckPassed = true,
                PepCheckPassed = false,
                AdverseMediaCheckPassed = true,
                RiskScore = 65,
                RiskLevel = "Medium",
                ScreeningStatus = "Review Required",
                PepMatchDetails = "Director is a politically exposed person",
                SanctionsListsChecked = "OFAC, EU, UN, UK"
            }
        });

        _beneficialOwners.AddRange(new[]
        {
            new BeneficialOwner
            {
                EntityId = "buyer-001",
                EntityType = "Buyer",
                FullName = "John Smith",
                Nationality = "United Kingdom",
                CountryOfResidence = "Zambia",
                DateOfBirth = new DateTime(1970, 5, 15),
                IdentificationType = "Passport",
                IdentificationNumber = "UK123456789",
                IdentificationCountry = "United Kingdom",
                OwnershipPercentage = 60,
                IsDirectOwnership = true,
                HasVotingRights = true,
                VotingRightsPercentage = 60,
                IsPep = false,
                VerificationStatus = "Verified",
                VerificationDate = DateTime.Now.AddMonths(-3),
                VerifiedBy = "Compliance Officer"
            }
        });
    }

    public Task<IEnumerable<AmlScreeningResult>> GetAllScreeningResultsAsync()
        => Task.FromResult<IEnumerable<AmlScreeningResult>>(_screeningResults);

    public Task<AmlScreeningResult?> GetScreeningResultByIdAsync(string id)
        => Task.FromResult(_screeningResults.FirstOrDefault(s => s.Id == id));

    public Task<IEnumerable<AmlScreeningResult>> GetScreeningResultsByEntityAsync(string entityId, string entityType)
        => Task.FromResult<IEnumerable<AmlScreeningResult>>(
            _screeningResults.Where(s => s.EntityId == entityId && s.EntityType == entityType));

    public Task<AmlScreeningResult> CreateScreeningResultAsync(AmlScreeningResult result)
    {
        result.Id = Guid.NewGuid().ToString();
        _screeningResults.Add(result);
        return Task.FromResult(result);
    }

    public Task<AmlScreeningResult> UpdateScreeningResultAsync(AmlScreeningResult result)
    {
        var existing = _screeningResults.FirstOrDefault(s => s.Id == result.Id);
        if (existing != null)
        {
            _screeningResults.Remove(existing);
            _screeningResults.Add(result);
        }
        return Task.FromResult(result);
    }

    public Task<AmlScreeningResult> PerformScreeningAsync(string entityId, string entityType)
    {
        var result = new AmlScreeningResult
        {
            EntityId = entityId,
            EntityType = entityType,
            ScreeningDate = DateTime.Now,
            SanctionsCheckPassed = true,
            PepCheckPassed = true,
            AdverseMediaCheckPassed = true,
            RiskScore = new Random().Next(10, 40),
            RiskLevel = "Low",
            ScreeningStatus = "Passed",
            SanctionsListsChecked = "OFAC, EU, UN, UK",
            ExpiryDate = DateTime.Now.AddYears(1)
        };
        _screeningResults.Add(result);
        return Task.FromResult(result);
    }

    public Task<IEnumerable<BeneficialOwner>> GetBeneficialOwnersByEntityAsync(string entityId, string entityType)
        => Task.FromResult<IEnumerable<BeneficialOwner>>(
            _beneficialOwners.Where(o => o.EntityId == entityId && o.EntityType == entityType));

    public Task<BeneficialOwner?> GetBeneficialOwnerByIdAsync(string id)
        => Task.FromResult(_beneficialOwners.FirstOrDefault(o => o.Id == id));

    public Task<BeneficialOwner> CreateBeneficialOwnerAsync(BeneficialOwner owner)
    {
        owner.Id = Guid.NewGuid().ToString();
        owner.CreatedDate = DateTime.Now;
        _beneficialOwners.Add(owner);
        return Task.FromResult(owner);
    }

    public Task<BeneficialOwner> UpdateBeneficialOwnerAsync(BeneficialOwner owner)
    {
        var existing = _beneficialOwners.FirstOrDefault(o => o.Id == owner.Id);
        if (existing != null)
        {
            _beneficialOwners.Remove(existing);
            owner.LastUpdated = DateTime.Now;
            _beneficialOwners.Add(owner);
        }
        return Task.FromResult(owner);
    }

    public Task<bool> DeleteBeneficialOwnerAsync(string id)
    {
        var owner = _beneficialOwners.FirstOrDefault(o => o.Id == id);
        if (owner != null)
        {
            _beneficialOwners.Remove(owner);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<BeneficialOwner> VerifyBeneficialOwnerAsync(string id, string verifiedBy)
    {
        var owner = _beneficialOwners.FirstOrDefault(o => o.Id == id);
        if (owner != null)
        {
            owner.VerificationStatus = "Verified";
            owner.VerificationDate = DateTime.Now;
            owner.VerifiedBy = verifiedBy;
        }
        return Task.FromResult(owner!);
    }

    public Task<IEnumerable<SuspiciousActivityReport>> GetAllSarsAsync()
        => Task.FromResult<IEnumerable<SuspiciousActivityReport>>(_sars);

    public Task<SuspiciousActivityReport?> GetSarByIdAsync(string id)
        => Task.FromResult(_sars.FirstOrDefault(s => s.Id == id));

    public Task<IEnumerable<SuspiciousActivityReport>> GetSarsByStatusAsync(string status)
        => Task.FromResult<IEnumerable<SuspiciousActivityReport>>(_sars.Where(s => s.Status == status));

    public Task<SuspiciousActivityReport> CreateSarAsync(SuspiciousActivityReport report)
    {
        report.Id = Guid.NewGuid().ToString();
        report.ReportNumber = $"SAR-{DateTime.Now:yyyyMMdd}-{_sars.Count + 1:D4}";
        report.ReportDate = DateTime.Now;
        _sars.Add(report);
        return Task.FromResult(report);
    }

    public Task<SuspiciousActivityReport> UpdateSarAsync(SuspiciousActivityReport report)
    {
        var existing = _sars.FirstOrDefault(s => s.Id == report.Id);
        if (existing != null)
        {
            _sars.Remove(existing);
            _sars.Add(report);
        }
        return Task.FromResult(report);
    }

    public Task<SuspiciousActivityReport> FileSarAsync(string id, string filedBy)
    {
        var report = _sars.FirstOrDefault(s => s.Id == id);
        if (report != null)
        {
            report.Status = "Filed";
            report.FilingDate = DateTime.Now;
            report.ReviewedBy = filedBy;
            report.RegulatoryReference = $"FIU-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
        }
        return Task.FromResult(report!);
    }
}

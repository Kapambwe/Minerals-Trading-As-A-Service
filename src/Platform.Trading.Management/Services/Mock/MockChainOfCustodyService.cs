using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.ChainOfCustody;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IChainOfCustodyService for development and testing.
/// </summary>
public class MockChainOfCustodyService : IChainOfCustodyService
{
    private readonly List<CustodyRecord> _custodyRecords = new();
    private readonly List<AssayCertificate> _assayCertificates = new();

    public MockChainOfCustodyService()
    {
        SeedData();
    }

    private void SeedData()
    {
        var chainId = "COC-2024-001";
        var batchNumber = "BATCH-CU-2024-0001";

        // Create a sample custody chain from mine to warehouse
        _custodyRecords.AddRange(new[]
        {
            new CustodyRecord
            {
                CustodyChainId = chainId,
                BatchNumber = batchNumber,
                SequenceNumber = 1,
                MetalType = MetalType.Copper,
                Quantity = 100,
                QualityGrade = "Grade A",
                LotNumber = "LOT-001",
                CustodyType = "Mine",
                LocationType = "Mine Site",
                LocationName = "Konkola Copper Mine",
                LocationAddress = "Chililabombwe, Zambia",
                GpsCoordinates = "-12.3456, 27.7890",
                TransferringParty = "Konkola Copper Mines PLC",
                ReceivingParty = "Zambia Transport Ltd",
                TransferDate = DateTime.Now.AddDays(-5),
                ReceivedDate = DateTime.Now.AddDays(-5),
                MiningLicenseNumber = "ML-ZM-2024-0123",
                MineOperator = "Konkola Copper Mines PLC",
                MineLocation = "Chililabombwe District",
                IsVerified = true,
                VerifiedBy = "Inspector J. Banda",
                VerificationDate = DateTime.Now.AddDays(-5),
                VerifiedQuantity = 100,
                ConflictFreeVerified = true,
                ConflictMineralsComplianceStatus = "Compliant",
                OecdDueDiligenceLevel = "Step 5 - Complete",
                Status = "Verified"
            },
            new CustodyRecord
            {
                CustodyChainId = chainId,
                BatchNumber = batchNumber,
                SequenceNumber = 2,
                MetalType = MetalType.Copper,
                Quantity = 100,
                QualityGrade = "Grade A",
                LotNumber = "LOT-001",
                CustodyType = "Transport",
                LocationType = "Truck",
                LocationName = "Truck ZM-2024-ABC",
                LocationAddress = "En Route to Ndola",
                TransferringParty = "Zambia Transport Ltd",
                ReceivingParty = "ZME Approved Warehouse",
                TransferDate = DateTime.Now.AddDays(-4),
                ReceivedDate = DateTime.Now.AddDays(-3),
                TransportDocumentNumber = "TDN-2024-00567",
                WeightSlipNumber = "WS-2024-00234",
                IsVerified = true,
                VerifiedBy = "Driver M. Phiri",
                VerificationDate = DateTime.Now.AddDays(-3),
                VerifiedQuantity = 99.8m,
                QuantityVariance = -0.2m,
                ConflictFreeVerified = true,
                Status = "Verified"
            },
            new CustodyRecord
            {
                CustodyChainId = chainId,
                BatchNumber = batchNumber,
                SequenceNumber = 3,
                MetalType = MetalType.Copper,
                Quantity = 99.8m,
                QualityGrade = "Grade A",
                LotNumber = "LOT-001",
                CustodyType = "Warehouse",
                LocationType = "Warehouse",
                LocationName = "ZME Ndola Warehouse",
                LocationAddress = "Industrial Area, Ndola, Zambia",
                GpsCoordinates = "-12.9456, 28.6543",
                TransferringParty = "Zambia Transport Ltd",
                ReceivingParty = "ZME Warehouse Operations",
                TransferDate = DateTime.Now.AddDays(-3),
                ReceivedDate = DateTime.Now.AddDays(-3),
                WeightSlipNumber = "WS-WHR-2024-00098",
                IsVerified = true,
                VerifiedBy = "Warehouse Manager K. Mwansa",
                VerificationDate = DateTime.Now.AddDays(-3),
                VerifiedQuantity = 99.8m,
                ConflictFreeVerified = true,
                Status = "Verified"
            }
        });

        // Create assay certificate
        _assayCertificates.Add(new AssayCertificate
        {
            CertificateNumber = "ASSAY-2024-00456",
            IssueDate = DateTime.Now.AddDays(-4),
            SampleId = "SAMPLE-CU-001",
            BatchNumber = batchNumber,
            LotNumber = "LOT-001",
            SamplingDate = DateTime.Now.AddDays(-5),
            SamplingLocation = "Konkola Copper Mine",
            SampledBy = "Lab Tech P. Tembo",
            MetalType = MetalType.Copper,
            SampleWeight = 2.5m,
            GrossWeight = 100,
            LaboratoryName = "Zambia Bureau of Standards Testing Lab",
            LaboratoryAddress = "Plot 5032, Independence Ave, Lusaka",
            LaboratoryAccreditation = "ISO 17025:2017",
            AnalystName = "Dr. S. Mulenga",
            AnalysisDate = DateTime.Now.AddDays(-4),
            PrimaryMetalContent = 99.97m,
            QualityGrade = "LME Grade A",
            ElementalAnalysis = new List<AssayElement>
            {
                new AssayElement { Element = "Cu", ElementName = "Copper", Content = 99.97m, Unit = "Percentage", Method = "ICP-OES" },
                new AssayElement { Element = "Ag", ElementName = "Silver", Content = 15, Unit = "ppm", Method = "ICP-MS" },
                new AssayElement { Element = "Au", ElementName = "Gold", Content = 0.5m, Unit = "ppm", Method = "Fire Assay" }
            },
            Impurities = new List<ImpurityResult>
            {
                new ImpurityResult { Element = "O", ElementName = "Oxygen", Level = 50, Unit = "ppm", MaxAllowedLevel = 400, IsWithinSpec = true },
                new ImpurityResult { Element = "S", ElementName = "Sulfur", Level = 15, Unit = "ppm", MaxAllowedLevel = 40, IsWithinSpec = true },
                new ImpurityResult { Element = "Fe", ElementName = "Iron", Level = 10, Unit = "ppm", MaxAllowedLevel = 100, IsWithinSpec = true }
            },
            PhysicalForm = "Cathode",
            Dimensions = "1000mm x 1000mm x 15mm",
            Moisture = 0.02m,
            Status = "Issued",
            MeetsLmeStandard = true,
            MeetsZbsStandard = true,
            CustodyRecordId = _custodyRecords[0].Id
        });
    }

    // Custody Records
    public Task<IEnumerable<CustodyRecord>> GetAllCustodyRecordsAsync()
        => Task.FromResult<IEnumerable<CustodyRecord>>(_custodyRecords);

    public Task<CustodyRecord?> GetCustodyRecordByIdAsync(string id)
        => Task.FromResult(_custodyRecords.FirstOrDefault(c => c.Id == id));

    public Task<IEnumerable<CustodyRecord>> GetCustodyChainAsync(string custodyChainId)
        => Task.FromResult<IEnumerable<CustodyRecord>>(
            _custodyRecords.Where(c => c.CustodyChainId == custodyChainId).OrderBy(c => c.SequenceNumber));

    public Task<IEnumerable<CustodyRecord>> GetCustodyRecordsByBatchAsync(string batchNumber)
        => Task.FromResult<IEnumerable<CustodyRecord>>(
            _custodyRecords.Where(c => c.BatchNumber == batchNumber).OrderBy(c => c.SequenceNumber));

    public Task<CustodyRecord> CreateCustodyRecordAsync(CustodyRecord record)
    {
        record.Id = Guid.NewGuid().ToString();
        record.CreatedDate = DateTime.Now;
        _custodyRecords.Add(record);
        return Task.FromResult(record);
    }

    public Task<CustodyRecord> UpdateCustodyRecordAsync(CustodyRecord record)
    {
        var existing = _custodyRecords.FirstOrDefault(c => c.Id == record.Id);
        if (existing != null)
        {
            _custodyRecords.Remove(existing);
            record.LastUpdated = DateTime.Now;
            _custodyRecords.Add(record);
        }
        return Task.FromResult(record);
    }

    public Task<CustodyRecord> VerifyCustodyRecordAsync(string id, string verifiedBy, decimal verifiedQuantity)
    {
        var record = _custodyRecords.FirstOrDefault(c => c.Id == id);
        if (record != null)
        {
            record.IsVerified = true;
            record.VerifiedBy = verifiedBy;
            record.VerificationDate = DateTime.Now;
            record.VerifiedQuantity = verifiedQuantity;
            record.QuantityVariance = verifiedQuantity - record.Quantity;
            record.Status = "Verified";
            record.LastUpdated = DateTime.Now;
        }
        return Task.FromResult(record!);
    }

    public Task<bool> DeleteCustodyRecordAsync(string id)
    {
        var record = _custodyRecords.FirstOrDefault(c => c.Id == id);
        if (record != null)
        {
            _custodyRecords.Remove(record);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    // Assay Certificates
    public Task<IEnumerable<AssayCertificate>> GetAllAssayCertificatesAsync()
        => Task.FromResult<IEnumerable<AssayCertificate>>(_assayCertificates);

    public Task<AssayCertificate?> GetAssayCertificateByIdAsync(string id)
        => Task.FromResult(_assayCertificates.FirstOrDefault(a => a.Id == id));

    public Task<AssayCertificate?> GetAssayCertificateByCertificateNumberAsync(string certificateNumber)
        => Task.FromResult(_assayCertificates.FirstOrDefault(a => a.CertificateNumber == certificateNumber));

    public Task<IEnumerable<AssayCertificate>> GetAssayCertificatesByBatchAsync(string batchNumber)
        => Task.FromResult<IEnumerable<AssayCertificate>>(_assayCertificates.Where(a => a.BatchNumber == batchNumber));

    public Task<AssayCertificate> CreateAssayCertificateAsync(AssayCertificate certificate)
    {
        certificate.Id = Guid.NewGuid().ToString();
        certificate.CertificateNumber = $"ASSAY-{DateTime.Now:yyyyMMdd}-{_assayCertificates.Count + 1:D5}";
        certificate.IssueDate = DateTime.Now;
        _assayCertificates.Add(certificate);
        return Task.FromResult(certificate);
    }

    public Task<AssayCertificate> UpdateAssayCertificateAsync(AssayCertificate certificate)
    {
        var existing = _assayCertificates.FirstOrDefault(a => a.Id == certificate.Id);
        if (existing != null)
        {
            _assayCertificates.Remove(existing);
            _assayCertificates.Add(certificate);
        }
        return Task.FromResult(certificate);
    }

    public Task<AssayCertificate> VerifyAssayCertificateAsync(string id)
    {
        var certificate = _assayCertificates.FirstOrDefault(a => a.Id == id);
        if (certificate != null)
        {
            certificate.Status = "Verified";
        }
        return Task.FromResult(certificate!);
    }

    public Task<bool> DeleteAssayCertificateAsync(string id)
    {
        var certificate = _assayCertificates.FirstOrDefault(a => a.Id == id);
        if (certificate != null)
        {
            _assayCertificates.Remove(certificate);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    // Traceability
    public Task<IEnumerable<CustodyRecord>> TraceBackToMineAsync(string warrantId)
    {
        // In a real implementation, this would trace the warrant back through the custody chain
        // For mock, return the full chain of the first batch
        return Task.FromResult<IEnumerable<CustodyRecord>>(_custodyRecords.OrderBy(c => c.SequenceNumber));
    }

    public Task<bool> ValidateChainIntegrityAsync(string custodyChainId)
    {
        var chain = _custodyRecords
            .Where(c => c.CustodyChainId == custodyChainId)
            .OrderBy(c => c.SequenceNumber)
            .ToList();

        if (chain.Count == 0) return Task.FromResult(false);

        // Check sequence is complete
        for (int i = 0; i < chain.Count; i++)
        {
            if (chain[i].SequenceNumber != i + 1)
                return Task.FromResult(false);
        }

        // Check all records are verified
        if (chain.Any(c => !c.IsVerified))
            return Task.FromResult(false);

        return Task.FromResult(true);
    }

    public Task<bool> ValidateConflictMineralsComplianceAsync(string custodyChainId)
    {
        var chain = _custodyRecords.Where(c => c.CustodyChainId == custodyChainId);
        return Task.FromResult(chain.All(c => c.ConflictFreeVerified == true));
    }
}

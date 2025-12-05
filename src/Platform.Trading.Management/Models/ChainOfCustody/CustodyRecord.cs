namespace Platform.Trading.Management.Models.ChainOfCustody;

/// <summary>
/// Represents a point in the chain of custody for minerals from mine to market.
/// Addresses Gap WD-005: Complete traceability from mine to warehouse.
/// </summary>
public class CustodyRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CustodyChainId { get; set; } = string.Empty; // Groups all records for a batch
    public string BatchNumber { get; set; } = string.Empty;
    public int SequenceNumber { get; set; } // Order in the custody chain
    
    // Mineral Information
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // in metric tons
    public string QualityGrade { get; set; } = string.Empty;
    public string? LotNumber { get; set; }
    
    // Location Information
    public string CustodyType { get; set; } = string.Empty; // Mine, Transport, Warehouse, Processing, Export
    public string LocationType { get; set; } = string.Empty; // Mine Site, Truck, Rail, Warehouse, Port
    public string LocationName { get; set; } = string.Empty;
    public string LocationAddress { get; set; } = string.Empty;
    public string? GpsCoordinates { get; set; }
    
    // Transfer Details
    public string TransferringParty { get; set; } = string.Empty;
    public string ReceivingParty { get; set; } = string.Empty;
    public DateTime TransferDate { get; set; } = DateTime.Now;
    public DateTime? ReceivedDate { get; set; }
    
    // Documentation
    public string? TransportDocumentNumber { get; set; } // Bill of Lading, Waybill, etc.
    public string? WeightSlipNumber { get; set; }
    public string? AssayCertificateId { get; set; }
    
    // Verification
    public bool IsVerified { get; set; }
    public string? VerifiedBy { get; set; }
    public DateTime? VerificationDate { get; set; }
    public decimal? VerifiedQuantity { get; set; }
    public decimal? QuantityVariance { get; set; }
    
    // Status
    public string Status { get; set; } = "In Transit"; // In Transit, Received, Verified, Disputed
    
    // Mining License (for mine origin)
    public string? MiningLicenseNumber { get; set; }
    public string? MineOperator { get; set; }
    public string? MineLocation { get; set; }
    
    // Conflict Minerals Compliance (RC-008)
    public bool? ConflictFreeVerified { get; set; }
    public string? ConflictMineralsComplianceStatus { get; set; }
    public string? OecdDueDiligenceLevel { get; set; } // Step 1-5 of OECD guidance
    
    public string? Notes { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastUpdated { get; set; }
}

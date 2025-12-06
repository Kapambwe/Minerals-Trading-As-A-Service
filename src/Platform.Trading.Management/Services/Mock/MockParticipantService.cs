using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Participant;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IParticipantService for development and testing.
/// </summary>
public class MockParticipantService : IParticipantService
{
    private readonly List<ArtisanalMiner> _artisanalMiners = new();
    private readonly List<AggregationCenter> _aggregationCenters = new();
    private readonly List<DisputeCase> _disputeCases = new();
    private readonly List<MineralAuction> _auctions = new();

    public MockParticipantService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed aggregation center
        _aggregationCenters.Add(new AggregationCenter
        {
            CenterCode = "AGC-001",
            CenterName = "Copperbelt ASM Collection Center",
            EstablishedDate = DateTime.Now.AddYears(-1),
            Province = "Copperbelt",
            District = "Kitwe",
            Address = "Plot 123, Industrial Area, Kitwe",
            GpsCoordinates = "-12.8024, 28.2132",
            ManagerName = "James Mwale",
            PhoneNumber = "+260971234567",
            Email = "kitwe.center@zme.zm",
            MineralsAccepted = new List<MetalType> { MetalType.Copper, MetalType.Cobalt },
            StorageCapacity = 100,
            CurrentStock = 25,
            HasWeighingEquipment = true,
            HasQualityTestingEquipment = true,
            LastEquipmentCalibration = DateTime.Now.AddMonths(-1),
            RegisteredMiners = 45,
            MonthlyThroughput = 5000,
            ZemaApproved = true,
            ZemaCertificateNumber = "ZEMA-AGC-2024-001",
            MiningMinistryApproved = true,
            MiningLicenseNumber = "ML-AGC-2024-001",
            Status = "Active"
        });

        // Seed artisanal miner
        _artisanalMiners.Add(new ArtisanalMiner
        {
            RegistrationNumber = "ASM-2024-00001",
            RegistrationDate = DateTime.Now.AddMonths(-6),
            MinerType = "Cooperative",
            Name = "Mwanza Mining Cooperative",
            CooperativeName = "Mwanza Mining Cooperative",
            MemberCount = 15,
            PhoneNumber = "+260955123456",
            Email = "mwanza.coop@gmail.com",
            Province = "Copperbelt",
            District = "Kitwe",
            Village = "Mwanza Township",
            GpsCoordinates = "-12.7950, 28.2050",
            IdType = "NRC",
            IdNumber = "123456/78/1",
            IdVerified = true,
            IdVerifiedDate = DateTime.Now.AddMonths(-6),
            ArtisanalMiningLicense = "AML-2024-00567",
            LicenseIssueDate = DateTime.Now.AddMonths(-12),
            LicenseExpiryDate = DateTime.Now.AddYears(1),
            LicenseVerified = true,
            LicenseStatus = "Valid",
            MineralsProduced = new List<MetalType> { MetalType.Copper },
            MiningMethod = "SemiMechanized",
            EstimatedMonthlyProduction = 500,
            MiningAreaDescription = "3 hectare alluvial copper deposit near Kafue River",
            AggregationCenterId = "agc-001",
            AggregationCenterName = "Copperbelt ASM Collection Center",
            HasBankAccount = false,
            MobileMoneyProvider = "MTN_MoMo",
            MobileMoneyNumber = "+260955123456",
            PreferredPaymentMethod = "MobileMoney",
            ResponsibleMiningCertified = true,
            ResponsibleMiningCertDate = DateTime.Now.AddMonths(-3),
            ConflictFreeVerified = true,
            LastVerificationDate = DateTime.Now.AddMonths(-1),
            OecdDueDiligenceLevel = "Compliant",
            Status = "Approved",
            ApprovalDate = DateTime.Now.AddMonths(-6),
            ApprovedBy = "ASM Registration Officer",
            TotalLifetimeProduction = 3000,
            TotalLifetimeValue = 25500,
            TrainingRecords = new List<AsmTrainingRecord>
            {
                new AsmTrainingRecord
                {
                    TrainingName = "Responsible Mining Practices",
                    TrainingType = "ResponsibleMining",
                    Provider = "Alliance for Responsible Mining",
                    TrainingDate = DateTime.Now.AddMonths(-3),
                    Completed = true,
                    CertificateNumber = "ARM-ZM-2024-001"
                }
            },
            ProductionHistory = new List<AsmProductionRecord>
            {
                new AsmProductionRecord
                {
                    ProductionDate = DateTime.Now.AddMonths(-1),
                    MetalType = MetalType.Copper,
                    Quantity = 500,
                    QualityGrade = "Standard",
                    DeliveryReceiptNumber = "DLV-2024-001",
                    DeliveryDate = DateTime.Now.AddMonths(-1),
                    UnitPrice = 8.50m,
                    TotalValue = 4250,
                    PaymentCurrency = "USD",
                    PaymentStatus = "Paid",
                    PaymentDate = DateTime.Now.AddDays(-25),
                    PaymentReference = "MM-2024-001"
                }
            }
        });

        // Seed dispute case
        _disputeCases.Add(new DisputeCase
        {
            CaseNumber = "DSP-2024-00001",
            FiledDate = DateTime.Now.AddDays(-30),
            ClaimantId = "buyer-001",
            ClaimantName = "Copper Trading Ltd",
            ClaimantType = "Buyer",
            RespondentId = "seller-001",
            RespondentName = "Zambia Mining Corp",
            RespondentType = "Seller",
            DisputeType = "QualityDispute",
            DisputeCategory = "Quality",
            Subject = "Copper Grade Discrepancy",
            Description = "The delivered copper grade was below the contracted Grade A specification. Assay results show 99.5% purity vs contracted 99.99%.",
            TradeId = "trade-002",
            TradeNumber = "TRD-2024-002",
            DisputedAmount = 50000,
            DisputedCurrency = "USD",
            DisputedQuantity = 50,
            Status = "Mediation",
            StatusDate = DateTime.Now.AddDays(-15),
            AssignedMediatorId = "mediator-001",
            AssignedMediatorName = "John Mwamba",
            MediationStartDate = DateTime.Now.AddDays(-10),
            FilingFee = 500,
            FeeCurrency = "USD",
            FeePaidBy = "Claimant",
            Documents = new List<DisputeDocument>
            {
                new DisputeDocument
                {
                    DocumentType = "Claim",
                    DocumentName = "Initial Complaint",
                    SubmittedDate = DateTime.Now.AddDays(-30),
                    SubmittedBy = "Copper Trading Ltd"
                },
                new DisputeDocument
                {
                    DocumentType = "Evidence",
                    DocumentName = "Assay Certificate",
                    SubmittedDate = DateTime.Now.AddDays(-28),
                    SubmittedBy = "Copper Trading Ltd"
                },
                new DisputeDocument
                {
                    DocumentType = "Response",
                    DocumentName = "Seller's Response",
                    SubmittedDate = DateTime.Now.AddDays(-20),
                    SubmittedBy = "Zambia Mining Corp"
                }
            },
            Events = new List<DisputeEvent>
            {
                new DisputeEvent
                {
                    EventDate = DateTime.Now.AddDays(-30),
                    EventType = "CaseFiled",
                    Description = "Dispute case filed by Copper Trading Ltd"
                },
                new DisputeEvent
                {
                    EventDate = DateTime.Now.AddDays(-25),
                    EventType = "ResponseReceived",
                    Description = "Response received from Zambia Mining Corp"
                },
                new DisputeEvent
                {
                    EventDate = DateTime.Now.AddDays(-15),
                    EventType = "MediatorAssigned",
                    Description = "John Mwamba assigned as mediator"
                },
                new DisputeEvent
                {
                    EventDate = DateTime.Now.AddDays(-10),
                    EventType = "MediationStarted",
                    Description = "Mediation proceedings commenced"
                }
            }
        });

        // Seed auction
        _auctions.Add(new MineralAuction
        {
            AuctionNumber = "AUC-2024-00001",
            AuctionType = "SealedBid",
            Title = "ZCCM-IH Copper Cathode Auction - Q1 2024",
            Description = "Quarterly auction of copper cathodes from ZCCM-IH stockpile",
            AuctionAuthority = "ZCCM-IH",
            AnnouncementDate = DateTime.Now.AddDays(-30),
            BiddingStartDate = DateTime.Now.AddDays(-7),
            BiddingEndDate = DateTime.Now.AddDays(7),
            TotalLots = 5,
            TotalQuantity = 500,
            TotalReservePrice = 4000000,
            Currency = "USD",
            ParticipantCount = 12,
            ParticipationDeposit = 50000,
            DepositCurrency = "USD",
            Status = "Open",
            SampleViewingStart = DateTime.Now.AddDays(-10),
            SampleViewingEnd = DateTime.Now.AddDays(-5),
            ViewingLocation = "ZCCM-IH Warehouse, Kitwe",
            TermsAndConditions = "Standard ZCCM-IH auction terms apply. Payment within 5 business days of award. Collection within 30 days.",
            Lots = new List<AuctionLot>
            {
                new AuctionLot
                {
                    LotNumber = "LOT-001",
                    MetalType = MetalType.Copper,
                    Quantity = 100,
                    QualityGrade = "Grade A (LME)",
                    AssayCertificateNumber = "ASSAY-2024-001",
                    ReservePrice = 850000,
                    IncrementAmount = 10000,
                    Currency = "USD",
                    WarehouseId = "wh-zccm-001",
                    WarehouseName = "ZCCM-IH Central Warehouse",
                    WarrantNumber = "WRT-ZCCM-2024-001",
                    OriginMine = "Konkola Copper Mines",
                    OriginProvince = "Copperbelt",
                    ConflictFreeVerified = true,
                    Status = "Bidding",
                    BidCount = 5,
                    CurrentHighestBid = 880000
                },
                new AuctionLot
                {
                    LotNumber = "LOT-002",
                    MetalType = MetalType.Copper,
                    Quantity = 100,
                    QualityGrade = "Grade A (LME)",
                    AssayCertificateNumber = "ASSAY-2024-002",
                    ReservePrice = 850000,
                    IncrementAmount = 10000,
                    Currency = "USD",
                    WarehouseId = "wh-zccm-001",
                    WarehouseName = "ZCCM-IH Central Warehouse",
                    OriginMine = "Mopani Copper Mines",
                    OriginProvince = "Copperbelt",
                    ConflictFreeVerified = true,
                    Status = "Bidding",
                    BidCount = 3,
                    CurrentHighestBid = 860000
                }
            },
            RegisteredParticipants = new List<AuctionParticipant>
            {
                new AuctionParticipant
                {
                    ParticipantId = "buyer-001",
                    ParticipantName = "Copper Trading Ltd",
                    ParticipantType = "Trader",
                    RegistrationDate = DateTime.Now.AddDays(-20),
                    DepositPaid = true,
                    DepositAmount = 50000,
                    QualificationVerified = true,
                    IsActive = true
                }
            }
        });
    }

    // Artisanal Miners
    public Task<IEnumerable<ArtisanalMiner>> GetAllArtisanalMinersAsync()
        => Task.FromResult<IEnumerable<ArtisanalMiner>>(_artisanalMiners);

    public Task<ArtisanalMiner?> GetArtisanalMinerByIdAsync(string id)
        => Task.FromResult(_artisanalMiners.FirstOrDefault(m => m.Id == id));

    public Task<IEnumerable<ArtisanalMiner>> GetArtisanalMinersByStatusAsync(string status)
        => Task.FromResult<IEnumerable<ArtisanalMiner>>(_artisanalMiners.Where(m => m.Status == status));

    public Task<IEnumerable<ArtisanalMiner>> GetArtisanalMinersByProvinceAsync(string province)
        => Task.FromResult<IEnumerable<ArtisanalMiner>>(_artisanalMiners.Where(m => m.Province == province));

    public Task<IEnumerable<ArtisanalMiner>> GetArtisanalMinersByAggregationCenterAsync(string centerId)
        => Task.FromResult<IEnumerable<ArtisanalMiner>>(_artisanalMiners.Where(m => m.AggregationCenterId == centerId));

    public Task<ArtisanalMiner> RegisterArtisanalMinerAsync(ArtisanalMiner miner)
    {
        miner.Id = Guid.NewGuid().ToString();
        miner.RegistrationNumber = $"ASM-{DateTime.Now:yyyy}-{_artisanalMiners.Count + 1:D5}";
        miner.RegistrationDate = DateTime.Now;
        miner.Status = "Pending";
        _artisanalMiners.Add(miner);
        return Task.FromResult(miner);
    }

    public Task<ArtisanalMiner> UpdateArtisanalMinerAsync(ArtisanalMiner miner)
    {
        var existing = _artisanalMiners.FirstOrDefault(m => m.Id == miner.Id);
        if (existing != null)
        {
            _artisanalMiners.Remove(existing);
            _artisanalMiners.Add(miner);
        }
        return Task.FromResult(miner);
    }

    public Task<ArtisanalMiner> ApproveArtisanalMinerAsync(string id, string approvedBy)
    {
        var miner = _artisanalMiners.FirstOrDefault(m => m.Id == id);
        if (miner != null)
        {
            miner.Status = "Approved";
            miner.ApprovalDate = DateTime.Now;
            miner.ApprovedBy = approvedBy;
        }
        return Task.FromResult(miner!);
    }

    public Task<ArtisanalMiner> SuspendArtisanalMinerAsync(string id, string reason)
    {
        var miner = _artisanalMiners.FirstOrDefault(m => m.Id == id);
        if (miner != null)
        {
            miner.Status = "Suspended";
            miner.SuspensionReason = reason;
        }
        return Task.FromResult(miner!);
    }

    public Task<AsmProductionRecord> RecordProductionAsync(string minerId, AsmProductionRecord record)
    {
        var miner = _artisanalMiners.FirstOrDefault(m => m.Id == minerId);
        if (miner != null)
        {
            record.Id = Guid.NewGuid().ToString();
            miner.ProductionHistory.Add(record);
            miner.TotalLifetimeProduction += record.Quantity;
            miner.TotalLifetimeValue += record.TotalValue;
        }
        return Task.FromResult(record);
    }

    public Task<AsmTrainingRecord> RecordTrainingAsync(string minerId, AsmTrainingRecord record)
    {
        var miner = _artisanalMiners.FirstOrDefault(m => m.Id == minerId);
        if (miner != null)
        {
            record.Id = Guid.NewGuid().ToString();
            miner.TrainingRecords.Add(record);
        }
        return Task.FromResult(record);
    }

    public Task<bool> VerifyConflictFreeStatusAsync(string minerId)
    {
        var miner = _artisanalMiners.FirstOrDefault(m => m.Id == minerId);
        return Task.FromResult(miner?.ConflictFreeVerified ?? false);
    }

    // Aggregation Centers
    public Task<IEnumerable<AggregationCenter>> GetAllAggregationCentersAsync()
        => Task.FromResult<IEnumerable<AggregationCenter>>(_aggregationCenters);

    public Task<AggregationCenter?> GetAggregationCenterByIdAsync(string id)
        => Task.FromResult(_aggregationCenters.FirstOrDefault(c => c.Id == id));

    public Task<IEnumerable<AggregationCenter>> GetAggregationCentersByProvinceAsync(string province)
        => Task.FromResult<IEnumerable<AggregationCenter>>(_aggregationCenters.Where(c => c.Province == province));

    public Task<AggregationCenter> CreateAggregationCenterAsync(AggregationCenter center)
    {
        center.Id = Guid.NewGuid().ToString();
        center.CenterCode = $"AGC-{_aggregationCenters.Count + 1:D3}";
        _aggregationCenters.Add(center);
        return Task.FromResult(center);
    }

    public Task<AggregationCenter> UpdateAggregationCenterAsync(AggregationCenter center)
    {
        var existing = _aggregationCenters.FirstOrDefault(c => c.Id == center.Id);
        if (existing != null)
        {
            _aggregationCenters.Remove(existing);
            _aggregationCenters.Add(center);
        }
        return Task.FromResult(center);
    }

    public Task<IEnumerable<ArtisanalMiner>> GetMinersAtCenterAsync(string centerId)
        => Task.FromResult<IEnumerable<ArtisanalMiner>>(_artisanalMiners.Where(m => m.AggregationCenterId == centerId));

    // Dispute Resolution
    public Task<IEnumerable<DisputeCase>> GetAllDisputeCasesAsync()
        => Task.FromResult<IEnumerable<DisputeCase>>(_disputeCases);

    public Task<DisputeCase?> GetDisputeCaseByIdAsync(string id)
        => Task.FromResult(_disputeCases.FirstOrDefault(d => d.Id == id));

    public Task<IEnumerable<DisputeCase>> GetDisputeCasesByStatusAsync(string status)
        => Task.FromResult<IEnumerable<DisputeCase>>(_disputeCases.Where(d => d.Status == status));

    public Task<IEnumerable<DisputeCase>> GetDisputeCasesByPartyAsync(string partyId)
        => Task.FromResult<IEnumerable<DisputeCase>>(
            _disputeCases.Where(d => d.ClaimantId == partyId || d.RespondentId == partyId));

    public Task<DisputeCase> FileDisputeAsync(DisputeCase disputeCase)
    {
        disputeCase.Id = Guid.NewGuid().ToString();
        disputeCase.CaseNumber = $"DSP-{DateTime.Now:yyyy}-{_disputeCases.Count + 1:D5}";
        disputeCase.FiledDate = DateTime.Now;
        disputeCase.Status = "Filed";
        disputeCase.StatusDate = DateTime.Now;
        disputeCase.Events.Add(new DisputeEvent
        {
            EventDate = DateTime.Now,
            EventType = "CaseFiled",
            Description = $"Dispute case filed by {disputeCase.ClaimantName}"
        });
        _disputeCases.Add(disputeCase);
        return Task.FromResult(disputeCase);
    }

    public Task<DisputeCase> UpdateDisputeCaseAsync(DisputeCase disputeCase)
    {
        var existing = _disputeCases.FirstOrDefault(d => d.Id == disputeCase.Id);
        if (existing != null)
        {
            _disputeCases.Remove(existing);
            _disputeCases.Add(disputeCase);
        }
        return Task.FromResult(disputeCase);
    }

    public Task<DisputeCase> AssignMediatorAsync(string caseId, string mediatorId, string mediatorName)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            dispute.AssignedMediatorId = mediatorId;
            dispute.AssignedMediatorName = mediatorName;
            dispute.Events.Add(new DisputeEvent
            {
                EventDate = DateTime.Now,
                EventType = "MediatorAssigned",
                Description = $"{mediatorName} assigned as mediator"
            });
        }
        return Task.FromResult(dispute!);
    }

    public Task<DisputeCase> AssignArbitrationPanelAsync(string caseId, List<string> panelIds, List<string> panelNames)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            dispute.ArbitrationPanelIds = panelIds;
            dispute.ArbitrationPanelNames = panelNames;
            dispute.Events.Add(new DisputeEvent
            {
                EventDate = DateTime.Now,
                EventType = "ArbitrationPanelAssigned",
                Description = $"Arbitration panel assigned: {string.Join(", ", panelNames)}"
            });
        }
        return Task.FromResult(dispute!);
    }

    public Task<DisputeCase> StartMediationAsync(string caseId)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            dispute.Status = "Mediation";
            dispute.StatusDate = DateTime.Now;
            dispute.MediationStartDate = DateTime.Now;
            dispute.Events.Add(new DisputeEvent
            {
                EventDate = DateTime.Now,
                EventType = "MediationStarted",
                Description = "Mediation proceedings commenced"
            });
        }
        return Task.FromResult(dispute!);
    }

    public Task<DisputeCase> StartArbitrationAsync(string caseId)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            dispute.Status = "Arbitration";
            dispute.StatusDate = DateTime.Now;
            dispute.ArbitrationStartDate = DateTime.Now;
            dispute.MediationSuccessful = false;
            dispute.MediationEndDate = DateTime.Now;
            dispute.Events.Add(new DisputeEvent
            {
                EventDate = DateTime.Now,
                EventType = "ArbitrationStarted",
                Description = "Arbitration proceedings commenced"
            });
        }
        return Task.FromResult(dispute!);
    }

    public Task<DisputeCase> ResolveDisputeAsync(string caseId, string resolutionType, string summary, decimal? awardedAmount = null, string? winningParty = null)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            dispute.Status = "Resolved";
            dispute.StatusDate = DateTime.Now;
            dispute.ResolutionDate = DateTime.Now;
            dispute.ResolutionType = resolutionType;
            dispute.ResolutionSummary = summary;
            dispute.AwardedAmount = awardedAmount;
            dispute.WinningParty = winningParty;
            dispute.Events.Add(new DisputeEvent
            {
                EventDate = DateTime.Now,
                EventType = "CaseResolved",
                Description = $"Case resolved via {resolutionType}: {summary}"
            });
        }
        return Task.FromResult(dispute!);
    }

    public Task<DisputeCase> WithdrawDisputeAsync(string caseId, string reason)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            dispute.Status = "Withdrawn";
            dispute.StatusDate = DateTime.Now;
            dispute.ResolutionDate = DateTime.Now;
            dispute.ResolutionType = "Withdrawn";
            dispute.ResolutionSummary = reason;
            dispute.Events.Add(new DisputeEvent
            {
                EventDate = DateTime.Now,
                EventType = "CaseWithdrawn",
                Description = $"Case withdrawn: {reason}"
            });
        }
        return Task.FromResult(dispute!);
    }

    public Task<DisputeCase> AppealDisputeAsync(string caseId)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            dispute.IsAppealed = true;
            dispute.AppealDate = DateTime.Now;
            dispute.AppealCaseNumber = $"APL-{DateTime.Now:yyyy}-{caseId.Substring(0, 4)}";
            dispute.Status = "Appealed";
            dispute.StatusDate = DateTime.Now;
            dispute.Events.Add(new DisputeEvent
            {
                EventDate = DateTime.Now,
                EventType = "AppealFiled",
                Description = $"Appeal filed, case number: {dispute.AppealCaseNumber}"
            });
        }
        return Task.FromResult(dispute!);
    }

    public Task<DisputeDocument> AddDocumentAsync(string caseId, DisputeDocument document)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            document.Id = Guid.NewGuid().ToString();
            document.SubmittedDate = DateTime.Now;
            dispute.Documents.Add(document);
        }
        return Task.FromResult(document);
    }

    public Task<DisputeEvent> AddEventAsync(string caseId, DisputeEvent @event)
    {
        var dispute = _disputeCases.FirstOrDefault(d => d.Id == caseId);
        if (dispute != null)
        {
            @event.Id = Guid.NewGuid().ToString();
            @event.EventDate = DateTime.Now;
            dispute.Events.Add(@event);
        }
        return Task.FromResult(@event);
    }

    // Auctions
    public Task<IEnumerable<MineralAuction>> GetAllAuctionsAsync()
        => Task.FromResult<IEnumerable<MineralAuction>>(_auctions);

    public Task<MineralAuction?> GetAuctionByIdAsync(string id)
        => Task.FromResult(_auctions.FirstOrDefault(a => a.Id == id));

    public Task<IEnumerable<MineralAuction>> GetAuctionsByStatusAsync(string status)
        => Task.FromResult<IEnumerable<MineralAuction>>(_auctions.Where(a => a.Status == status));

    public Task<IEnumerable<MineralAuction>> GetUpcomingAuctionsAsync()
        => Task.FromResult<IEnumerable<MineralAuction>>(
            _auctions.Where(a => a.BiddingStartDate > DateTime.Now || a.Status == "Announced" || a.Status == "Open"));

    public Task<MineralAuction> CreateAuctionAsync(MineralAuction auction)
    {
        auction.Id = Guid.NewGuid().ToString();
        auction.AuctionNumber = $"AUC-{DateTime.Now:yyyy}-{_auctions.Count + 1:D5}";
        auction.Status = "Announced";
        auction.StatusDate = DateTime.Now;
        _auctions.Add(auction);
        return Task.FromResult(auction);
    }

    public Task<MineralAuction> UpdateAuctionAsync(MineralAuction auction)
    {
        var existing = _auctions.FirstOrDefault(a => a.Id == auction.Id);
        if (existing != null)
        {
            _auctions.Remove(existing);
            _auctions.Add(auction);
        }
        return Task.FromResult(auction);
    }

    public Task<MineralAuction> OpenAuctionAsync(string auctionId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            auction.Status = "Open";
            auction.StatusDate = DateTime.Now;
            foreach (var lot in auction.Lots)
            {
                lot.Status = "Bidding";
            }
        }
        return Task.FromResult(auction!);
    }

    public Task<MineralAuction> CloseAuctionAsync(string auctionId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            auction.Status = "Closed";
            auction.StatusDate = DateTime.Now;
            
            // Process results
            auction.LotsAwarded = auction.Lots.Count(l => l.WinningBidderId != null);
            auction.LotsUnsold = auction.Lots.Count(l => l.WinningBidderId == null);
            auction.TotalSoldValue = auction.Lots.Where(l => l.WinningBidAmount.HasValue).Sum(l => l.WinningBidAmount!.Value);
            auction.TotalSoldQuantity = auction.Lots.Where(l => l.WinningBidderId != null).Sum(l => l.Quantity);
        }
        return Task.FromResult(auction!);
    }

    public Task<MineralAuction> CancelAuctionAsync(string auctionId, string reason)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            auction.Status = "Cancelled";
            auction.StatusDate = DateTime.Now;
            auction.Notes = $"Cancelled: {reason}";
        }
        return Task.FromResult(auction!);
    }

    public Task<MineralAuction> ExtendAuctionAsync(string auctionId, DateTime newEndDate)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            auction.IsExtended = true;
            auction.ExtendedEndDate = newEndDate;
        }
        return Task.FromResult(auction!);
    }

    // Auction Lots
    public Task<AuctionLot> AddLotToAuctionAsync(string auctionId, AuctionLot lot)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            lot.Id = Guid.NewGuid().ToString();
            lot.LotNumber = $"LOT-{auction.Lots.Count + 1:D3}";
            lot.Status = "Available";
            auction.Lots.Add(lot);
            auction.TotalLots = auction.Lots.Count;
            auction.TotalQuantity = auction.Lots.Sum(l => l.Quantity);
            auction.TotalReservePrice = auction.Lots.Sum(l => l.ReservePrice);
        }
        return Task.FromResult(lot);
    }

    public Task<AuctionLot> UpdateLotAsync(string auctionId, AuctionLot lot)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            var existing = auction.Lots.FirstOrDefault(l => l.Id == lot.Id);
            if (existing != null)
            {
                auction.Lots.Remove(existing);
                auction.Lots.Add(lot);
            }
        }
        return Task.FromResult(lot);
    }

    public Task<AuctionLot> WithdrawLotAsync(string auctionId, string lotId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        var lot = auction?.Lots.FirstOrDefault(l => l.Id == lotId);
        if (lot != null)
        {
            lot.Status = "Withdrawn";
        }
        return Task.FromResult(lot!);
    }

    public Task<IEnumerable<AuctionLot>> GetLotsForAuctionAsync(string auctionId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        return Task.FromResult<IEnumerable<AuctionLot>>(auction?.Lots ?? new List<AuctionLot>());
    }

    // Auction Participants
    public Task<AuctionParticipant> RegisterParticipantAsync(string auctionId, AuctionParticipant participant)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        if (auction != null)
        {
            participant.Id = Guid.NewGuid().ToString();
            participant.RegistrationDate = DateTime.Now;
            auction.RegisteredParticipants.Add(participant);
            auction.ParticipantCount = auction.RegisteredParticipants.Count;
        }
        return Task.FromResult(participant);
    }

    public Task<AuctionParticipant> VerifyParticipantAsync(string auctionId, string participantId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        var participant = auction?.RegisteredParticipants.FirstOrDefault(p => p.ParticipantId == participantId);
        if (participant != null)
        {
            participant.QualificationVerified = true;
        }
        return Task.FromResult(participant!);
    }

    public Task<AuctionParticipant> DisqualifyParticipantAsync(string auctionId, string participantId, string reason)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        var participant = auction?.RegisteredParticipants.FirstOrDefault(p => p.ParticipantId == participantId);
        if (participant != null)
        {
            participant.IsActive = false;
            participant.DisqualificationReason = reason;
        }
        return Task.FromResult(participant!);
    }

    // Bidding
    public Task<AuctionBid> PlaceBidAsync(string auctionId, string lotId, string bidderId, decimal bidAmount)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        var lot = auction?.Lots.FirstOrDefault(l => l.Id == lotId);
        var participant = auction?.RegisteredParticipants.FirstOrDefault(p => p.ParticipantId == bidderId);
        
        var bid = new AuctionBid
        {
            BidTime = DateTime.Now,
            BidderId = bidderId,
            BidderName = participant?.ParticipantName ?? "Unknown",
            BidAmount = bidAmount,
            Currency = lot?.Currency ?? "USD",
            IsValid = bidAmount >= (lot?.ReservePrice ?? 0) && (lot?.CurrentHighestBid == null || bidAmount > lot.CurrentHighestBid)
        };

        if (lot != null && bid.IsValid)
        {
            lot.Bids.Add(bid);
            lot.BidCount = lot.Bids.Count;
            lot.CurrentHighestBid = bidAmount;
            lot.CurrentHighestBidderId = bidderId;
        }
        else
        {
            bid.IsValid = false;
            bid.InvalidationReason = "Bid amount is below reserve price or current highest bid";
        }

        return Task.FromResult(bid);
    }

    public Task<IEnumerable<AuctionBid>> GetBidsForLotAsync(string auctionId, string lotId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        var lot = auction?.Lots.FirstOrDefault(l => l.Id == lotId);
        return Task.FromResult<IEnumerable<AuctionBid>>(lot?.Bids ?? new List<AuctionBid>());
    }

    public Task<AuctionLot> AwardLotAsync(string auctionId, string lotId, string winningBidderId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        var lot = auction?.Lots.FirstOrDefault(l => l.Id == lotId);
        var participant = auction?.RegisteredParticipants.FirstOrDefault(p => p.ParticipantId == winningBidderId);
        
        if (lot != null)
        {
            lot.Status = "Awarded";
            lot.WinningBidderId = winningBidderId;
            lot.WinningBidderName = participant?.ParticipantName;
            lot.WinningBidAmount = lot.Bids.FirstOrDefault(b => b.BidderId == winningBidderId)?.BidAmount ?? lot.CurrentHighestBid;
            lot.AwardedDate = DateTime.Now;

            var winningBid = lot.Bids.FirstOrDefault(b => b.BidderId == winningBidderId);
            if (winningBid != null) winningBid.IsWinningBid = true;
        }
        return Task.FromResult(lot!);
    }

    public Task<AuctionLot> MarkLotUnsoldAsync(string auctionId, string lotId)
    {
        var auction = _auctions.FirstOrDefault(a => a.Id == auctionId);
        var lot = auction?.Lots.FirstOrDefault(l => l.Id == lotId);
        if (lot != null)
        {
            lot.Status = "Unsold";
        }
        return Task.FromResult(lot!);
    }
}

using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Clearing;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IClearingService for development and testing.
/// </summary>
public class MockClearingService : IClearingService
{
    private readonly List<ClearingMember> _clearingMembers = new();
    private readonly List<DefaultManagement> _defaultCases = new();
    private readonly List<RtgsTransaction> _rtgsTransactions = new();
    private readonly List<DvpSettlement> _dvpSettlements = new();
    private readonly List<SettlementCycle> _settlementCycles = new();
    private readonly List<GuaranteeFundContribution> _contributions = new();
    private GuaranteeFund _guaranteeFund = new();

    public MockClearingService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed clearing members
        _clearingMembers.AddRange(new[]
        {
            new ClearingMember
            {
                MemberNumber = "CM-001",
                MemberName = "Zambia Mining Corp",
                MemberType = "General",
                MemberSince = DateTime.Now.AddYears(-2),
                Status = "Active",
                MinimumCapitalRequirement = 5000000,
                CurrentCapital = 7500000,
                GuaranteeFundContribution = 500000,
                MaxGrossPosition = 50000000,
                MaxNetPosition = 25000000,
                CurrentGrossPosition = 15000000,
                CurrentNetPosition = 5000000,
                CreditRating = "BBB",
                CreditRatingAgency = "S&P"
            },
            new ClearingMember
            {
                MemberNumber = "CM-002",
                MemberName = "Copper Trading Ltd",
                MemberType = "Direct",
                MemberSince = DateTime.Now.AddYears(-1),
                Status = "Active",
                MinimumCapitalRequirement = 2500000,
                CurrentCapital = 3000000,
                GuaranteeFundContribution = 250000,
                MaxGrossPosition = 25000000,
                MaxNetPosition = 12500000,
                CurrentGrossPosition = 8000000,
                CurrentNetPosition = 2000000,
                CreditRating = "BB+",
                CreditRatingAgency = "Moody's"
            }
        });

        // Seed guarantee fund
        _guaranteeFund = new GuaranteeFund
        {
            FundName = "ZME Clearing Guarantee Fund",
            AsOfDate = DateTime.UtcNow,
            TotalFundSize = 10000000,
            Currency = "USD",
            CcpSkinInTheGame = 1000000,
            MemberContributions = 8500000,
            RetainedEarnings = 500000,
            CurrentUtilization = 0,
            StressTestCoverage = 95,
            LastStressTestDate = DateTime.Now.AddDays(-30),
            StressTestResult = "Pass"
        };

        // Seed contributions
        _contributions.AddRange(new[]
        {
            new GuaranteeFundContribution
            {
                MemberId = "CM-001",
                MemberName = "Zambia Mining Corp",
                RequiredContribution = 500000,
                ActualContribution = 500000,
                Currency = "USD",
                ContributionDate = DateTime.Now.AddMonths(-1),
                CalculationMethod = "RiskBased",
                AverageRiskExposure = 15000000,
                TradingVolume = 50000000
            },
            new GuaranteeFundContribution
            {
                MemberId = "CM-002",
                MemberName = "Copper Trading Ltd",
                RequiredContribution = 250000,
                ActualContribution = 250000,
                Currency = "USD",
                ContributionDate = DateTime.Now.AddMonths(-1),
                CalculationMethod = "RiskBased",
                AverageRiskExposure = 8000000,
                TradingVolume = 25000000
            }
        });

        // Seed RTGS transaction
        _rtgsTransactions.Add(new RtgsTransaction
        {
            TransactionReference = "RTGS-2024-00001",
            InitiatedDate = DateTime.Now.AddDays(-1),
            SettledDate = DateTime.Now.AddDays(-1),
            TransactionType = "SettlementPayment",
            SenderBankCode = "ZANCZMLU",
            SenderBankName = "Zanaco Bank",
            SenderAccountNumber = "1234567890",
            SenderAccountName = "Copper Trading Ltd",
            ReceiverBankCode = "SBICZMLX",
            ReceiverBankName = "Stanbic Bank",
            ReceiverAccountNumber = "0987654321",
            ReceiverAccountName = "Zambia Mining Corp",
            Amount = 852500,
            Currency = "ZMW",
            TradeId = "trade-001",
            Status = "Settled",
            RtgsConfirmationNumber = "BOZ-RTGS-2024-12345",
            BozReference = "BOZ-2024-00567",
            BozConfirmationTime = DateTime.Now.AddDays(-1)
        });

        // Seed DvP settlement
        _dvpSettlements.Add(new DvpSettlement
        {
            SettlementReference = "DVP-2024-00001",
            ScheduledDate = DateTime.Now.AddDays(-1),
            CompletedDate = DateTime.Now.AddDays(-1),
            TradeId = "trade-001",
            TradeNumber = "TRD-2024-001",
            SellerId = "seller-001",
            SellerName = "Zambia Mining Corp",
            BuyerId = "buyer-001",
            BuyerName = "Copper Trading Ltd",
            MetalType = MetalType.Copper,
            Quantity = 100,
            QualityGrade = "Grade A",
            WarrantId = "warrant-001",
            WarrantNumber = "WRT-2024-001",
            WarehouseId = "wh-001",
            WarehouseName = "Kitwe Central Warehouse",
            DeliveryStatus = "Transferred",
            DeliveryBlockedTime = DateTime.Now.AddDays(-1).AddHours(-2),
            DeliveryReleasedTime = DateTime.Now.AddDays(-1),
            PaymentAmount = 852500,
            PaymentCurrency = "USD",
            PaymentMethod = "RTGS",
            RtgsTransactionId = "rtgs-001",
            PaymentStatus = "Released",
            PaymentEscrowedTime = DateTime.Now.AddDays(-1).AddHours(-2),
            PaymentReleasedTime = DateTime.Now.AddDays(-1),
            OverallStatus = "Completed"
        });
    }

    // Clearing Members
    public Task<IEnumerable<ClearingMember>> GetAllClearingMembersAsync()
        => Task.FromResult<IEnumerable<ClearingMember>>(_clearingMembers);

    public Task<ClearingMember?> GetClearingMemberByIdAsync(string id)
        => Task.FromResult(_clearingMembers.FirstOrDefault(m => m.Id == id));

    public Task<IEnumerable<ClearingMember>> GetClearingMembersByStatusAsync(string status)
        => Task.FromResult<IEnumerable<ClearingMember>>(_clearingMembers.Where(m => m.Status == status));

    public Task<ClearingMember> CreateClearingMemberAsync(ClearingMember member)
    {
        member.Id = Guid.NewGuid().ToString();
        member.MemberNumber = $"CM-{_clearingMembers.Count + 1:D3}";
        member.MemberSince = DateTime.Now;
        _clearingMembers.Add(member);
        return Task.FromResult(member);
    }

    public Task<ClearingMember> UpdateClearingMemberAsync(ClearingMember member)
    {
        var existing = _clearingMembers.FirstOrDefault(m => m.Id == member.Id);
        if (existing != null)
        {
            _clearingMembers.Remove(existing);
            _clearingMembers.Add(member);
        }
        return Task.FromResult(member);
    }

    public Task<ClearingMember> SuspendClearingMemberAsync(string id, string reason)
    {
        var member = _clearingMembers.FirstOrDefault(m => m.Id == id);
        if (member != null)
        {
            member.Status = "Suspended";
            member.SuspensionDate = DateTime.Now;
            member.SuspensionReason = reason;
        }
        return Task.FromResult(member!);
    }

    public Task<ClearingMember> ReactivateClearingMemberAsync(string id)
    {
        var member = _clearingMembers.FirstOrDefault(m => m.Id == id);
        if (member != null)
        {
            member.Status = "Active";
            member.SuspensionDate = null;
            member.SuspensionReason = null;
        }
        return Task.FromResult(member!);
    }

    public Task<bool> ValidateMemberCapitalRequirementsAsync(string memberId)
    {
        var member = _clearingMembers.FirstOrDefault(m => m.Id == memberId);
        return Task.FromResult(member?.MeetsCapitalRequirement ?? false);
    }

    // Guarantee Fund
    public Task<GuaranteeFund> GetGuaranteeFundStatusAsync()
        => Task.FromResult(_guaranteeFund);

    public Task<IEnumerable<GuaranteeFundContribution>> GetMemberContributionsAsync()
        => Task.FromResult<IEnumerable<GuaranteeFundContribution>>(_contributions);

    public Task<GuaranteeFundContribution?> GetMemberContributionAsync(string memberId)
        => Task.FromResult(_contributions.FirstOrDefault(c => c.MemberId == memberId));

    public Task<GuaranteeFundContribution> RecalculateMemberContributionAsync(string memberId)
    {
        var contribution = _contributions.FirstOrDefault(c => c.MemberId == memberId);
        if (contribution != null)
        {
            // Simplified calculation based on risk exposure
            contribution.RequiredContribution = contribution.AverageRiskExposure * 0.05m; // 5% of exposure
            contribution.NextReviewDate = DateTime.Now.AddMonths(3);
        }
        return Task.FromResult(contribution!);
    }

    public Task<GuaranteeFundContribution> RecordContributionPaymentAsync(string memberId, decimal amount, DateTime paymentDate)
    {
        var contribution = _contributions.FirstOrDefault(c => c.MemberId == memberId);
        if (contribution != null)
        {
            contribution.ActualContribution += amount;
            contribution.ContributionDate = paymentDate;
        }
        return Task.FromResult(contribution!);
    }

    public Task<decimal> CalculateTotalGuaranteeFundAsync()
    {
        var total = _contributions.Sum(c => c.ActualContribution) + _guaranteeFund.CcpSkinInTheGame + _guaranteeFund.RetainedEarnings;
        return Task.FromResult(total);
    }

    // Default Management
    public Task<IEnumerable<DefaultManagement>> GetAllDefaultCasesAsync()
        => Task.FromResult<IEnumerable<DefaultManagement>>(_defaultCases);

    public Task<DefaultManagement?> GetDefaultCaseByIdAsync(string id)
        => Task.FromResult(_defaultCases.FirstOrDefault(d => d.Id == id));

    public Task<IEnumerable<DefaultManagement>> GetActiveDefaultCasesAsync()
        => Task.FromResult<IEnumerable<DefaultManagement>>(_defaultCases.Where(d => d.Status != "Closed"));

    public Task<DefaultManagement> DeclareDefaultAsync(string memberId, string defaultType, decimal defaultAmount, string description)
    {
        var member = _clearingMembers.FirstOrDefault(m => m.Id == memberId);
        var defaultCase = new DefaultManagement
        {
            CaseNumber = $"DEF-{DateTime.Now:yyyy}-{_defaultCases.Count + 1:D4}",
            DefaultDate = DateTime.Now,
            DefaultingMemberId = memberId,
            DefaultingMemberName = member?.MemberName ?? "Unknown",
            DefaultType = defaultType,
            DefaultAmount = defaultAmount,
            Currency = "USD",
            Description = description,
            Status = "Declared",
            TotalLoss = defaultAmount,
            Events = new List<DefaultManagementEvent>
            {
                new DefaultManagementEvent
                {
                    EventDate = DateTime.Now,
                    EventType = "DefaultDeclared",
                    Description = $"Default declared: {description}",
                    ActionTaken = "Default management procedures initiated"
                }
            }
        };
        _defaultCases.Add(defaultCase);

        if (member != null)
        {
            member.Status = "Default";
        }

        return Task.FromResult(defaultCase);
    }

    public Task<DefaultManagement> UpdateDefaultCaseStatusAsync(string id, string status)
    {
        var defaultCase = _defaultCases.FirstOrDefault(d => d.Id == id);
        if (defaultCase != null)
        {
            defaultCase.Status = status;
            defaultCase.Events.Add(new DefaultManagementEvent
            {
                EventDate = DateTime.Now,
                EventType = "StatusUpdate",
                Description = $"Status changed to {status}"
            });
        }
        return Task.FromResult(defaultCase!);
    }

    public Task<DefaultManagement> ApplyWaterfallAsync(string caseId)
    {
        var defaultCase = _defaultCases.FirstOrDefault(d => d.Id == caseId);
        if (defaultCase != null)
        {
            var waterfall = GetDefaultWaterfallAsync(defaultCase.DefaultingMemberId, defaultCase.TotalLoss).Result;
            defaultCase.WaterfallApplication = waterfall;
            defaultCase.Status = "LossAllocation";

            decimal remainingLoss = defaultCase.TotalLoss;
            foreach (var layer in waterfall)
            {
                if (remainingLoss <= 0) break;
                var utilizationAmount = Math.Min(remainingLoss, layer.AvailableAmount);
                layer.UtilizedAmount = utilizationAmount;
                layer.PercentageOfLossCovered = (utilizationAmount / defaultCase.TotalLoss) * 100;
                layer.ApplicationDate = DateTime.Now;
                remainingLoss -= utilizationAmount;
                defaultCase.RecoveredAmount += utilizationAmount;
            }
        }
        return Task.FromResult(defaultCase!);
    }

    public Task<DefaultManagement> InitiatePortfolioAuctionAsync(string caseId)
    {
        var defaultCase = _defaultCases.FirstOrDefault(d => d.Id == caseId);
        if (defaultCase != null)
        {
            defaultCase.PortfolioAuctioned = true;
            defaultCase.AuctionDate = DateTime.Now.AddDays(3);
            defaultCase.Status = "PortfolioAuction";
            defaultCase.Events.Add(new DefaultManagementEvent
            {
                EventDate = DateTime.Now,
                EventType = "PortfolioAuction",
                Description = "Portfolio auction initiated",
                ActionTaken = $"Auction scheduled for {defaultCase.AuctionDate:yyyy-MM-dd}"
            });
        }
        return Task.FromResult(defaultCase!);
    }

    public Task<DefaultManagement> CloseDefaultCaseAsync(string caseId, string resolution)
    {
        var defaultCase = _defaultCases.FirstOrDefault(d => d.Id == caseId);
        if (defaultCase != null)
        {
            defaultCase.Status = "Closed";
            defaultCase.ResolutionDate = DateTime.Now;
            defaultCase.Events.Add(new DefaultManagementEvent
            {
                EventDate = DateTime.Now,
                EventType = "CaseClosed",
                Description = resolution
            });
        }
        return Task.FromResult(defaultCase!);
    }

    public Task<List<WaterfallLayer>> GetDefaultWaterfallAsync(string memberId, decimal lossAmount)
    {
        var memberContribution = _contributions.FirstOrDefault(c => c.MemberId == memberId);
        
        return Task.FromResult(new List<WaterfallLayer>
        {
            new WaterfallLayer
            {
                Order = 1,
                LayerName = "Defaulter's Margin",
                LayerType = "DefaulterMargin",
                AvailableAmount = lossAmount * 0.3m // Assume 30% margin
            },
            new WaterfallLayer
            {
                Order = 2,
                LayerName = "Defaulter's Guarantee Fund Contribution",
                LayerType = "DefaulterGuaranteeFund",
                AvailableAmount = memberContribution?.ActualContribution ?? 0
            },
            new WaterfallLayer
            {
                Order = 3,
                LayerName = "CCP Skin-in-the-Game",
                LayerType = "CcpSkinInTheGame",
                AvailableAmount = _guaranteeFund.CcpSkinInTheGame
            },
            new WaterfallLayer
            {
                Order = 4,
                LayerName = "Non-Defaulting Members' Guarantee Fund",
                LayerType = "NonDefaulterGuaranteeFund",
                AvailableAmount = _guaranteeFund.MemberContributions - (memberContribution?.ActualContribution ?? 0)
            },
            new WaterfallLayer
            {
                Order = 5,
                LayerName = "CCP Capital",
                LayerType = "CcpCapital",
                AvailableAmount = 5000000 // CCP's additional capital
            }
        });
    }

    // RTGS Integration
    public Task<IEnumerable<RtgsTransaction>> GetAllRtgsTransactionsAsync()
        => Task.FromResult<IEnumerable<RtgsTransaction>>(_rtgsTransactions);

    public Task<RtgsTransaction?> GetRtgsTransactionByIdAsync(string id)
        => Task.FromResult(_rtgsTransactions.FirstOrDefault(t => t.Id == id));

    public Task<IEnumerable<RtgsTransaction>> GetRtgsTransactionsByStatusAsync(string status)
        => Task.FromResult<IEnumerable<RtgsTransaction>>(_rtgsTransactions.Where(t => t.Status == status));

    public Task<RtgsTransaction> InitiateRtgsPaymentAsync(RtgsTransaction transaction)
    {
        transaction.Id = Guid.NewGuid().ToString();
        transaction.TransactionReference = $"RTGS-{DateTime.Now:yyyy}-{_rtgsTransactions.Count + 1:D5}";
        transaction.InitiatedDate = DateTime.UtcNow;
        transaction.Status = "Submitted";
        _rtgsTransactions.Add(transaction);
        return Task.FromResult(transaction);
    }

    public Task<RtgsTransaction> UpdateRtgsStatusAsync(string id, string status, string? confirmationNumber = null)
    {
        var transaction = _rtgsTransactions.FirstOrDefault(t => t.Id == id);
        if (transaction != null)
        {
            transaction.Status = status;
            if (status == "Settled")
            {
                transaction.SettledDate = DateTime.UtcNow;
                transaction.RtgsConfirmationNumber = confirmationNumber ?? $"BOZ-RTGS-{DateTime.Now:yyyyMMddHHmmss}";
                transaction.BozConfirmationTime = DateTime.UtcNow;
            }
        }
        return Task.FromResult(transaction!);
    }

    public Task<RtgsTransaction?> GetRtgsTransactionByTradeAsync(string tradeId)
        => Task.FromResult(_rtgsTransactions.FirstOrDefault(t => t.TradeId == tradeId));

    // DvP Settlement
    public Task<IEnumerable<DvpSettlement>> GetAllDvpSettlementsAsync()
        => Task.FromResult<IEnumerable<DvpSettlement>>(_dvpSettlements);

    public Task<DvpSettlement?> GetDvpSettlementByIdAsync(string id)
        => Task.FromResult(_dvpSettlements.FirstOrDefault(s => s.Id == id));

    public Task<DvpSettlement?> GetDvpSettlementByTradeAsync(string tradeId)
        => Task.FromResult(_dvpSettlements.FirstOrDefault(s => s.TradeId == tradeId));

    public Task<IEnumerable<DvpSettlement>> GetDvpSettlementsByStatusAsync(string status)
        => Task.FromResult<IEnumerable<DvpSettlement>>(_dvpSettlements.Where(s => s.OverallStatus == status));

    public Task<DvpSettlement> InitiateDvpSettlementAsync(string tradeId)
    {
        var settlement = new DvpSettlement
        {
            SettlementReference = $"DVP-{DateTime.Now:yyyy}-{_dvpSettlements.Count + 1:D5}",
            ScheduledDate = DateTime.Now.AddDays(2), // T+2
            TradeId = tradeId,
            OverallStatus = "Pending"
        };
        _dvpSettlements.Add(settlement);
        return Task.FromResult(settlement);
    }

    public Task<DvpSettlement> BlockDeliveryAsync(string settlementId)
    {
        var settlement = _dvpSettlements.FirstOrDefault(s => s.Id == settlementId);
        if (settlement != null)
        {
            settlement.DeliveryStatus = "Blocked";
            settlement.DeliveryBlockedTime = DateTime.UtcNow;
            settlement.OverallStatus = "InProgress";
        }
        return Task.FromResult(settlement!);
    }

    public Task<DvpSettlement> EscrowPaymentAsync(string settlementId)
    {
        var settlement = _dvpSettlements.FirstOrDefault(s => s.Id == settlementId);
        if (settlement != null)
        {
            settlement.PaymentStatus = "Escrowed";
            settlement.PaymentEscrowedTime = DateTime.UtcNow;
        }
        return Task.FromResult(settlement!);
    }

    public Task<DvpSettlement> CompleteDvpSettlementAsync(string settlementId)
    {
        var settlement = _dvpSettlements.FirstOrDefault(s => s.Id == settlementId);
        if (settlement != null)
        {
            settlement.DeliveryStatus = "Transferred";
            settlement.DeliveryReleasedTime = DateTime.UtcNow;
            settlement.PaymentStatus = "Released";
            settlement.PaymentReleasedTime = DateTime.UtcNow;
            settlement.OverallStatus = "Completed";
            settlement.CompletedDate = DateTime.UtcNow;
        }
        return Task.FromResult(settlement!);
    }

    public Task<DvpSettlement> RollbackDvpSettlementAsync(string settlementId, string reason)
    {
        var settlement = _dvpSettlements.FirstOrDefault(s => s.Id == settlementId);
        if (settlement != null)
        {
            settlement.RolledBack = true;
            settlement.RollbackTime = DateTime.UtcNow;
            settlement.RollbackReason = reason;
            settlement.OverallStatus = "Rolled Back";
            settlement.DeliveryStatus = "Failed";
            settlement.PaymentStatus = "Failed";
            settlement.FailureReason = reason;
        }
        return Task.FromResult(settlement!);
    }

    // Settlement Cycle
    public Task<IEnumerable<SettlementCycle>> GetSettlementCyclesAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var query = _settlementCycles.AsEnumerable();
        if (fromDate.HasValue)
            query = query.Where(s => s.TradeDate >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(s => s.TradeDate <= toDate.Value);
        return Task.FromResult(query);
    }

    public Task<SettlementCycle> CreateSettlementCycleAsync(DateTime tradeDate, List<string> tradeIds, int settlementDays = 2)
    {
        var cycle = new SettlementCycle
        {
            TradeDate = tradeDate,
            SettlementDays = settlementDays,
            ScheduledSettlementDate = tradeDate.AddDays(settlementDays),
            TradeIds = tradeIds,
            Status = "Scheduled"
        };
        _settlementCycles.Add(cycle);
        return Task.FromResult(cycle);
    }

    public Task<IEnumerable<SettlementCycle>> GetOverdueSettlementsAsync()
        => Task.FromResult<IEnumerable<SettlementCycle>>(
            _settlementCycles.Where(s => s.ScheduledSettlementDate < DateTime.Now && s.Status != "Completed"));
}

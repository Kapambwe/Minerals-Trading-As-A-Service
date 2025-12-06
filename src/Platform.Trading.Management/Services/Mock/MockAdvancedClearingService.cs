using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Clearing;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of netting service.
/// </summary>
public class MockNettingService : INettingService
{
    private readonly List<NettingCalculation> _calculations = new();

    public Task<NettingCalculation?> GetNettingCalculationAsync(string id)
        => Task.FromResult(_calculations.FirstOrDefault(c => c.Id == id));

    public Task<List<NettingCalculation>> GetNettingCyclesAsync(DateTime settlementDate)
        => Task.FromResult(_calculations.Where(c => c.SettlementDate.Date == settlementDate.Date).ToList());

    public Task<NettingCalculation> CalculateNettingAsync(DateTime settlementDate, string nettingType)
    {
        var calculation = new NettingCalculation
        {
            Id = Guid.NewGuid().ToString(),
            NettingCycleId = $"NET-{settlementDate:yyyyMMdd}-001",
            SettlementDate = settlementDate,
            NettingType = nettingType,
            GrossPayables = 5000000m,
            GrossReceivables = 5000000m,
            GrossTradeCount = 150,
            NetPayables = 750000m,
            NetReceivables = 750000m,
            NetSettlementCount = 25,
            LiquiditySavings = 4250000m,
            Status = "Calculated"
        };
        _calculations.Add(calculation);
        return Task.FromResult(calculation);
    }

    public Task<NettingCalculation> ApplyNettingAsync(string calculationId, string approvedBy)
    {
        var calc = _calculations.FirstOrDefault(c => c.Id == calculationId);
        if (calc != null)
        {
            calc.Status = "Applied";
            calc.AppliedDate = DateTime.UtcNow;
            calc.ApprovedBy = approvedBy;
        }
        return Task.FromResult(calc ?? throw new InvalidOperationException("Calculation not found"));
    }

    public Task<decimal> GetNettingEfficiencyAsync(DateTime fromDate, DateTime toDate)
        => Task.FromResult(85m); // 85% efficiency
}

/// <summary>
/// Mock implementation of collateral management service.
/// </summary>
public class MockCollateralService : ICollateralService
{
    private readonly List<CollateralManagement> _accounts = new();
    private readonly List<CollateralEligibility> _eligibility = new();

    public MockCollateralService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _accounts.Add(new CollateralManagement
        {
            Id = Guid.NewGuid().ToString(),
            ParticipantId = "PART-001",
            ParticipantName = "Glencore International",
            AccountType = "Margin",
            TotalMarketValue = 10000000m,
            TotalHaircut = 500000m,
            TotalCollateralValue = 9500000m,
            RequiredCollateral = 8000000m,
            PassesConcentrationLimits = true,
            Holdings = new List<CollateralHolding>
            {
                new CollateralHolding
                {
                    Id = Guid.NewGuid().ToString(),
                    CollateralType = "Cash",
                    Description = "USD Cash",
                    Quantity = 5000000,
                    MarketPrice = 1,
                    MarketValue = 5000000,
                    Currency = "USD",
                    HaircutPercentage = 0,
                    IsEligible = true,
                    Status = "Active"
                },
                new CollateralHolding
                {
                    Id = Guid.NewGuid().ToString(),
                    CollateralType = "GovernmentBond",
                    Description = "Zambia Treasury Bill",
                    SecurityId = "ZM-TB-2024-001",
                    Quantity = 5000000,
                    MarketPrice = 1,
                    MarketValue = 5000000,
                    Currency = "USD",
                    HaircutPercentage = 10,
                    IsEligible = true,
                    Status = "Active"
                }
            }
        });

        _eligibility.AddRange(new[]
        {
            new CollateralEligibility { AssetClass = "Cash", AssetType = "USD", IsEligible = true, StandardHaircut = 0 },
            new CollateralEligibility { AssetClass = "Cash", AssetType = "ZMW", IsEligible = true, StandardHaircut = 5 },
            new CollateralEligibility { AssetClass = "GovernmentBond", AssetType = "ZambiaTBill", IsEligible = true, StandardHaircut = 10, MinimumCreditRating = "B-" },
            new CollateralEligibility { AssetClass = "Metal", AssetType = "LMEWarrant", IsEligible = true, StandardHaircut = 15 }
        });
    }

    public Task<CollateralManagement?> GetCollateralAccountAsync(string participantId)
        => Task.FromResult(_accounts.FirstOrDefault(a => a.ParticipantId == participantId));

    public Task<List<CollateralManagement>> GetAllCollateralAccountsAsync()
        => Task.FromResult(_accounts.ToList());

    public Task<CollateralHolding> PledgeCollateralAsync(string participantId, CollateralHolding holding)
    {
        var account = _accounts.FirstOrDefault(a => a.ParticipantId == participantId);
        if (account != null)
        {
            holding.Id = Guid.NewGuid().ToString();
            holding.PledgeDate = DateTime.UtcNow;
            holding.Status = "Active";
            account.Holdings.Add(holding);
        }
        return Task.FromResult(holding);
    }

    public Task<CollateralHolding> ReleaseCollateralAsync(string participantId, string holdingId)
    {
        var account = _accounts.FirstOrDefault(a => a.ParticipantId == participantId);
        var holding = account?.Holdings.FirstOrDefault(h => h.Id == holdingId);
        if (holding != null)
        {
            holding.Status = "Released";
            holding.ReleaseDate = DateTime.UtcNow;
        }
        return Task.FromResult(holding ?? throw new InvalidOperationException("Holding not found"));
    }

    public Task<CollateralHolding> SubstituteCollateralAsync(string participantId, string oldHoldingId, CollateralHolding newHolding)
    {
        var account = _accounts.FirstOrDefault(a => a.ParticipantId == participantId);
        if (account != null)
        {
            var oldHolding = account.Holdings.FirstOrDefault(h => h.Id == oldHoldingId);
            if (oldHolding != null)
            {
                oldHolding.Status = "Substituted";
                oldHolding.ReleaseDate = DateTime.UtcNow;
            }
            newHolding.Id = Guid.NewGuid().ToString();
            newHolding.Status = "Active";
            account.Holdings.Add(newHolding);
        }
        return Task.FromResult(newHolding);
    }

    public Task<decimal> CalculateHaircutAsync(CollateralHolding holding)
    {
        var eligibility = _eligibility.FirstOrDefault(e => e.AssetClass == holding.CollateralType);
        return Task.FromResult(eligibility?.StandardHaircut ?? 20m);
    }

    public Task<bool> CheckEligibilityAsync(CollateralHolding holding)
    {
        var eligibility = _eligibility.FirstOrDefault(e => e.AssetClass == holding.CollateralType);
        return Task.FromResult(eligibility?.IsEligible ?? false);
    }

    public Task<List<ConcentrationCheck>> CheckConcentrationLimitsAsync(string participantId)
    {
        return Task.FromResult(new List<ConcentrationCheck>
        {
            new ConcentrationCheck { CheckType = "SingleIssuer", Description = "Max 25% single issuer", CurrentConcentration = 20, LimitPercentage = 25 },
            new ConcentrationCheck { CheckType = "AssetClass", Description = "Max 50% non-cash", CurrentConcentration = 45, LimitPercentage = 50 }
        });
    }

    public Task RevaluateCollateralAsync(string participantId)
    {
        var account = _accounts.FirstOrDefault(a => a.ParticipantId == participantId);
        if (account != null)
        {
            account.AsOfDate = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }

    public Task<List<CollateralEligibility>> GetEligibilityCriteriaAsync()
        => Task.FromResult(_eligibility.ToList());
}

/// <summary>
/// Mock implementation of position limit service.
/// </summary>
public class MockPositionLimitService : IPositionLimitService
{
    private readonly List<PositionLimit> _limits = new();

    public MockPositionLimitService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _limits.Add(new PositionLimit
        {
            Id = Guid.NewGuid().ToString(),
            LimitScope = "Participant",
            ParticipantId = "PART-001",
            ParticipantName = "Glencore International",
            MetalType = MetalType.Copper,
            LimitType = "Absolute",
            MaxGrossLongPosition = 10000,
            MaxGrossShortPosition = 10000,
            MaxNetLongPosition = 5000,
            MaxNetShortPosition = 5000,
            CurrentGrossLong = 3500,
            CurrentGrossShort = 2000,
            CurrentNetPosition = 1500,
            UtilizationPercentage = 35,
            Status = "Active",
            EffectiveDate = DateTime.UtcNow.AddMonths(-6)
        });
    }

    public Task<PositionLimit?> GetPositionLimitAsync(string participantId, MetalType? metalType = null)
    {
        var query = _limits.Where(l => l.ParticipantId == participantId);
        if (metalType.HasValue)
            query = query.Where(l => l.MetalType == metalType);
        return Task.FromResult(query.FirstOrDefault());
    }

    public Task<List<PositionLimit>> GetAllPositionLimitsAsync()
        => Task.FromResult(_limits.ToList());

    public Task<PositionLimit> CreatePositionLimitAsync(PositionLimit limit)
    {
        limit.Id = Guid.NewGuid().ToString();
        _limits.Add(limit);
        return Task.FromResult(limit);
    }

    public Task<PositionLimit> UpdatePositionLimitAsync(PositionLimit limit)
    {
        var existing = _limits.FindIndex(l => l.Id == limit.Id);
        if (existing >= 0)
            _limits[existing] = limit;
        return Task.FromResult(limit);
    }

    public Task<bool> CheckPositionLimitAsync(string participantId, MetalType metalType, decimal quantity, string side)
    {
        var limit = _limits.FirstOrDefault(l => l.ParticipantId == participantId && l.MetalType == metalType);
        if (limit == null) return Task.FromResult(true);

        var newPosition = side == "Buy" 
            ? (limit.CurrentGrossLong ?? 0) + quantity 
            : (limit.CurrentGrossShort ?? 0) + quantity;
        var maxLimit = side == "Buy" ? limit.MaxGrossLongPosition : limit.MaxGrossShortPosition;
        
        return Task.FromResult(newPosition <= maxLimit);
    }

    public Task<List<PositionLimit>> GetBreachedLimitsAsync()
        => Task.FromResult(_limits.Where(l => l.IsBreached).ToList());

    public Task<PositionLimit> GrantExemptionAsync(string limitId, string reason, DateTime expiryDate, string approvedBy)
    {
        var limit = _limits.FirstOrDefault(l => l.Id == limitId);
        if (limit != null)
        {
            limit.HasExemption = true;
            limit.ExemptionReason = reason;
            limit.ExemptionExpiryDate = expiryDate;
        }
        return Task.FromResult(limit ?? throw new InvalidOperationException("Limit not found"));
    }
}

/// <summary>
/// Mock implementation of mark-to-market service.
/// </summary>
public class MockMarkToMarketService : IMarkToMarketService
{
    private readonly List<MarkToMarket> _mtms = new();

    public Task<MarkToMarket?> GetMtmAsync(string entityId, DateTime valuationDate)
        => Task.FromResult(_mtms.FirstOrDefault(m => m.EntityId == entityId && m.ValuationDate.Date == valuationDate.Date));

    public Task<List<MarkToMarket>> GetAllMtmAsync(DateTime valuationDate)
        => Task.FromResult(_mtms.Where(m => m.ValuationDate.Date == valuationDate.Date).ToList());

    public Task<MarkToMarket> CalculateMtmAsync(string entityId)
    {
        var mtm = new MarkToMarket
        {
            Id = Guid.NewGuid().ToString(),
            EntityId = entityId,
            EntityName = "Sample Entity",
            EntityType = "ClearingMember",
            PreviousDayValue = 10000000m,
            CurrentMarketValue = 10250000m,
            DailyPnL = 250000m,
            UnrealizedPnL = 250000m,
            PreviousMarginRequirement = 1000000m,
            CurrentMarginRequirement = 1025000m,
            MarginChange = 25000m,
            CurrentCollateral = 1500000m,
            MarginExcessDeficit = 475000m,
            MarginCallRequired = false,
            ProcessingStatus = "Completed"
        };
        _mtms.Add(mtm);
        return Task.FromResult(mtm);
    }

    public Task RunEndOfDayMtmAsync()
        => Task.CompletedTask;

    public Task RunIntradayMtmAsync()
        => Task.CompletedTask;

    public Task<List<MarkToMarket>> GetMarginCallsAsync()
        => Task.FromResult(_mtms.Where(m => m.MarginCallRequired).ToList());

    public Task ProcessMarginCallAsync(string mtmId)
    {
        var mtm = _mtms.FirstOrDefault(m => m.Id == mtmId);
        if (mtm != null)
            mtm.MarginCallStatus = "Paid";
        return Task.CompletedTask;
    }

    public Task ProcessVariationMarginAsync(string mtmId)
    {
        var mtm = _mtms.FirstOrDefault(m => m.Id == mtmId);
        if (mtm != null)
            mtm.VariationMarginPaid = true;
        return Task.CompletedTask;
    }
}

/// <summary>
/// Mock implementation of interest calculation service.
/// </summary>
public class MockInterestService : IInterestService
{
    private readonly List<InterestCalculation> _calculations = new();

    public Task<InterestCalculation?> GetInterestCalculationAsync(string id)
        => Task.FromResult(_calculations.FirstOrDefault(c => c.Id == id));

    public Task<List<InterestCalculation>> GetInterestCalculationsAsync(string participantId, string period)
        => Task.FromResult(_calculations.Where(c => c.ParticipantId == participantId && c.CalculationPeriod == period).ToList());

    public Task<List<InterestCalculation>> CalculateMonthlyInterestAsync(string period)
    {
        var calc = new InterestCalculation
        {
            Id = Guid.NewGuid().ToString(),
            CalculationPeriod = period,
            ParticipantId = "PART-001",
            ParticipantName = "Sample Participant",
            BalanceType = "ExcessMargin",
            AverageBalance = 500000m,
            InterestRate = 4.5m,
            Days = 30,
            InterestAmount = 1849.32m,
            Direction = "Credit"
        };
        _calculations.Add(calc);
        return Task.FromResult(new List<InterestCalculation> { calc });
    }

    public Task ApplyInterestAsync(string calculationId)
    {
        var calc = _calculations.FirstOrDefault(c => c.Id == calculationId);
        if (calc != null)
        {
            calc.Applied = true;
            calc.AppliedDate = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }

    public Task<decimal> GetCurrentInterestRateAsync(string balanceType)
        => Task.FromResult(balanceType == "ExcessMargin" ? 4.5m : 12.0m); // Lower rate for deposits, higher for late payments
}

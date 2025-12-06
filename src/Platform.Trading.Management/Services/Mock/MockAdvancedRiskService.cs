using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Risk;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of credit risk scoring service.
/// </summary>
public class MockCreditRiskService : ICreditRiskService
{
    private readonly List<CreditRiskScore> _scores = new();

    public MockCreditRiskService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _scores.Add(new CreditRiskScore
        {
            Id = Guid.NewGuid().ToString(),
            EntityId = "PART-001",
            EntityName = "Glencore International",
            EntityType = "ClearingMember",
            CreditScore = 85,
            CreditRating = "A",
            RiskCategory = "Low",
            CurrentRatio = 1.5m,
            DebtToEquityRatio = 0.8m,
            InterestCoverageRatio = 4.5m,
            TradingVolume30Days = 50000000m,
            AverageSettlementTime = 1.8m,
            PaymentPerformanceScore = 95m,
            RecommendedCreditLimit = 25000000m,
            CurrentCreditLimit = 20000000m,
            TrendDirection = "Stable",
            Components = new List<CreditScoreComponent>
            {
                new CreditScoreComponent { ComponentName = "Financial Health", Category = "Financial", Score = 88, Weight = 30, WeightedScore = 26.4m },
                new CreditScoreComponent { ComponentName = "Trading History", Category = "Trading", Score = 92, Weight = 25, WeightedScore = 23m },
                new CreditScoreComponent { ComponentName = "Payment Performance", Category = "Payment", Score = 95, Weight = 25, WeightedScore = 23.75m },
                new CreditScoreComponent { ComponentName = "External Rating", Category = "External", Score = 70, Weight = 20, WeightedScore = 14m }
            }
        });
    }

    public Task<CreditRiskScore?> GetCreditScoreAsync(string entityId)
        => Task.FromResult(_scores.FirstOrDefault(s => s.EntityId == entityId));

    public Task<List<CreditRiskScore>> GetAllCreditScoresAsync()
        => Task.FromResult(_scores.ToList());

    public Task<CreditRiskScore> CalculateCreditScoreAsync(string entityId)
    {
        var score = _scores.FirstOrDefault(s => s.EntityId == entityId);
        if (score == null)
        {
            score = new CreditRiskScore
            {
                Id = Guid.NewGuid().ToString(),
                EntityId = entityId,
                EntityName = "New Entity",
                CreditScore = 70,
                CreditRating = "BBB",
                RiskCategory = "Medium"
            };
            _scores.Add(score);
        }
        score.AssessmentDate = DateTime.UtcNow;
        return Task.FromResult(score);
    }

    public Task<CreditRiskScore> UpdateCreditScoreAsync(CreditRiskScore score)
    {
        var existing = _scores.FindIndex(s => s.Id == score.Id);
        if (existing >= 0)
            _scores[existing] = score;
        return Task.FromResult(score);
    }

    public Task<decimal> GetRecommendedCreditLimitAsync(string entityId)
    {
        var score = _scores.FirstOrDefault(s => s.EntityId == entityId);
        return Task.FromResult(score?.RecommendedCreditLimit ?? 1000000m);
    }

    public Task<List<CreditRiskScore>> GetHighRiskEntitiesAsync()
        => Task.FromResult(_scores.Where(s => s.RiskCategory == "High" || s.RiskCategory == "VeryHigh").ToList());

    public Task<List<CreditAlert>> GetActiveCreditAlertsAsync(string? entityId = null)
    {
        var alerts = new List<CreditAlert>();
        foreach (var score in _scores)
        {
            if (entityId == null || score.EntityId == entityId)
                alerts.AddRange(score.Alerts.Where(a => !a.Resolved));
        }
        return Task.FromResult(alerts);
    }

    public Task ScheduleCreditReviewAsync(string entityId, DateTime reviewDate)
    {
        var score = _scores.FirstOrDefault(s => s.EntityId == entityId);
        if (score != null)
            score.NextReviewDate = reviewDate;
        return Task.CompletedTask;
    }
}

/// <summary>
/// Mock implementation of liquidity risk service.
/// </summary>
public class MockLiquidityRiskService : ILiquidityRiskService
{
    private readonly List<LiquidityRisk> _risks = new();

    public MockLiquidityRiskService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _risks.Add(new LiquidityRisk
        {
            Id = Guid.NewGuid().ToString(),
            EntityId = "EXCHANGE",
            EntityName = "Zambia Metal Exchange",
            EntityType = "Exchange",
            AvailableLiquidity = 50000000m,
            Currency = "USD",
            GrossSettlementObligations = 30000000m,
            NetSettlementObligations = 15000000m,
            TotalLiquidityNeed = 20000000m,
            LiquidityCoverageRatio = 250m,
            MeetsMinimumCoverage = true,
            LiquidityGap = 30000000m,
            AlertLevel = "Normal",
            PassesStressTests = true,
            LiquiditySources = new List<LiquiditySource>
            {
                new LiquiditySource { SourceType = "Cash", SourceName = "Operating Cash", AvailableAmount = 30000000, UtilizedAmount = 0, RemainingCapacity = 30000000 },
                new LiquiditySource { SourceType = "CreditLine", SourceName = "Stanbic Bank Credit Facility", AvailableAmount = 20000000, UtilizedAmount = 0, RemainingCapacity = 20000000, AccessTimeMinutes = 30, CostBasisPoints = 25 }
            }
        });
    }

    public Task<LiquidityRisk?> GetLiquidityRiskAsync(string entityId)
        => Task.FromResult(_risks.FirstOrDefault(r => r.EntityId == entityId));

    public Task<List<LiquidityRisk>> GetAllLiquidityRisksAsync()
        => Task.FromResult(_risks.ToList());

    public Task<LiquidityRisk> CalculateLiquidityRiskAsync(string entityId)
    {
        var risk = _risks.FirstOrDefault(r => r.EntityId == entityId);
        if (risk == null)
        {
            risk = new LiquidityRisk
            {
                Id = Guid.NewGuid().ToString(),
                EntityId = entityId,
                AvailableLiquidity = 10000000m,
                TotalLiquidityNeed = 5000000m,
                LiquidityCoverageRatio = 200m,
                MeetsMinimumCoverage = true
            };
            _risks.Add(risk);
        }
        risk.AsOfDate = DateTime.UtcNow;
        return Task.FromResult(risk);
    }

    public Task<LiquidityRisk> RunIntradayMonitoringAsync(string entityId)
    {
        var risk = _risks.FirstOrDefault(r => r.EntityId == entityId);
        if (risk != null)
        {
            risk.MonitoringType = "Intraday";
            risk.AsOfDate = DateTime.UtcNow;
            risk.IntradayProfile.Add(new IntradayLiquidityPoint
            {
                Timestamp = DateTime.UtcNow,
                AvailableLiquidity = risk.AvailableLiquidity,
                CumulativeInflows = 5000000m,
                CumulativeOutflows = 3000000m,
                NetPosition = 2000000m,
                UsagePercentage = 40m
            });
        }
        return Task.FromResult(risk ?? throw new InvalidOperationException("Risk not found"));
    }

    public Task<List<LiquidityStressScenario>> RunLiquidityStressTestAsync(string entityId)
    {
        return Task.FromResult(new List<LiquidityStressScenario>
        {
            new LiquidityStressScenario
            {
                ScenarioName = "2008 Financial Crisis",
                ScenarioType = "HistoricalExtreme",
                StressedLiquidityNeed = 35000000m,
                StressedAvailability = 45000000m,
                CoverageUnderStress = 128m,
                PassesScenario = true
            },
            new LiquidityStressScenario
            {
                ScenarioName = "Double Default Scenario",
                ScenarioType = "HypotheticalSevere",
                StressedLiquidityNeed = 40000000m,
                StressedAvailability = 45000000m,
                CoverageUnderStress = 112m,
                PassesScenario = true
            }
        });
    }

    public Task<List<LiquidityAlert>> GetLiquidityAlertsAsync()
    {
        var alerts = new List<LiquidityAlert>();
        foreach (var risk in _risks)
            alerts.AddRange(risk.Alerts);
        return Task.FromResult(alerts);
    }

    public Task ActivateContingencyPlanAsync(string entityId, string measure)
    {
        var risk = _risks.FirstOrDefault(r => r.EntityId == entityId);
        if (risk != null)
            risk.ContingencyPlanActivated = true;
        return Task.CompletedTask;
    }

    public Task<List<LiquiditySource>> GetLiquiditySourcesAsync(string entityId)
    {
        var risk = _risks.FirstOrDefault(r => r.EntityId == entityId);
        return Task.FromResult(risk?.LiquiditySources ?? new List<LiquiditySource>());
    }
}

/// <summary>
/// Mock implementation of concentration limit service.
/// </summary>
public class MockConcentrationLimitService : IConcentrationLimitService
{
    private readonly List<ConcentrationLimit> _limits = new();

    public MockConcentrationLimitService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _limits.AddRange(new[]
        {
            new ConcentrationLimit
            {
                Id = Guid.NewGuid().ToString(),
                LimitType = "Counterparty",
                LimitName = "Single Counterparty Limit",
                LimitPercentage = 25,
                CurrentExposure = 15000000m,
                CurrentPercentage = 18,
                UtilizationPercentage = 72,
                AlertLevel = "Normal",
                EffectiveDate = DateTime.UtcNow.AddMonths(-12)
            },
            new ConcentrationLimit
            {
                Id = Guid.NewGuid().ToString(),
                LimitType = "Sector",
                LimitName = "Mining Sector Limit",
                LimitPercentage = 40,
                CurrentExposure = 30000000m,
                CurrentPercentage = 35,
                UtilizationPercentage = 87.5m,
                AlertLevel = "Warning",
                WarningThreshold = 80,
                EffectiveDate = DateTime.UtcNow.AddMonths(-12)
            },
            new ConcentrationLimit
            {
                Id = Guid.NewGuid().ToString(),
                LimitType = "MetalType",
                LimitName = "Copper Concentration Limit",
                LimitPercentage = 60,
                CurrentExposure = 45000000m,
                CurrentPercentage = 52,
                UtilizationPercentage = 86.7m,
                AlertLevel = "Warning",
                EffectiveDate = DateTime.UtcNow.AddMonths(-12)
            }
        });
    }

    public Task<ConcentrationLimit?> GetConcentrationLimitAsync(string id)
        => Task.FromResult(_limits.FirstOrDefault(l => l.Id == id));

    public Task<List<ConcentrationLimit>> GetAllConcentrationLimitsAsync()
        => Task.FromResult(_limits.ToList());

    public Task<List<ConcentrationLimit>> GetLimitsByTypeAsync(string limitType)
        => Task.FromResult(_limits.Where(l => l.LimitType == limitType).ToList());

    public Task<ConcentrationLimit> CreateConcentrationLimitAsync(ConcentrationLimit limit)
    {
        limit.Id = Guid.NewGuid().ToString();
        _limits.Add(limit);
        return Task.FromResult(limit);
    }

    public Task<ConcentrationLimit> UpdateConcentrationLimitAsync(ConcentrationLimit limit)
    {
        var existing = _limits.FindIndex(l => l.Id == limit.Id);
        if (existing >= 0)
            _limits[existing] = limit;
        return Task.FromResult(limit);
    }

    public Task<List<ConcentrationLimit>> CheckAllConcentrationsAsync()
    {
        foreach (var limit in _limits)
        {
            limit.IsBreached = limit.CurrentPercentage > limit.LimitPercentage;
            limit.AlertLevel = limit.UtilizationPercentage >= limit.CriticalThreshold ? "Critical"
                : limit.UtilizationPercentage >= limit.WarningThreshold ? "Warning"
                : "Normal";
        }
        return Task.FromResult(_limits.ToList());
    }

    public Task<List<ConcentrationLimit>> GetBreachedLimitsAsync()
        => Task.FromResult(_limits.Where(l => l.IsBreached).ToList());

    public Task<ConcentrationLimit> GrantExemptionAsync(string limitId, string reason, DateTime expiryDate, string approvedBy)
    {
        var limit = _limits.FirstOrDefault(l => l.Id == limitId);
        if (limit != null)
        {
            limit.HasExemption = true;
            limit.ExemptionReason = reason;
            limit.ExemptionExpiryDate = expiryDate;
            limit.ApprovedBy = approvedBy;
        }
        return Task.FromResult(limit ?? throw new InvalidOperationException("Limit not found"));
    }

    public Task<decimal> CalculateCurrentConcentrationAsync(string limitType, string? entityId = null)
    {
        var limit = _limits.FirstOrDefault(l => l.LimitType == limitType && (entityId == null || l.EntityId == entityId));
        return Task.FromResult(limit?.CurrentPercentage ?? 0);
    }
}

/// <summary>
/// Mock implementation of operational risk service.
/// </summary>
public class MockOperationalRiskService : IOperationalRiskService
{
    private readonly List<OperationalRisk> _risks = new();

    public MockOperationalRiskService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _risks.Add(new OperationalRisk
        {
            Id = Guid.NewGuid().ToString(),
            RiskCategory = "Systems",
            RiskSubCategory = "IT Infrastructure",
            RiskDescription = "Trading system outage risk",
            LikelihoodScore = 2,
            ImpactScore = 4,
            RiskLevel = "Medium",
            ResidualLikelihood = 1,
            ResidualImpact = 3,
            RiskOwnerId = "IT-001",
            RiskOwnerName = "IT Manager",
            Status = "Active",
            Controls = new List<RiskControl>
            {
                new RiskControl { ControlName = "Disaster Recovery", ControlType = "Corrective", Effectiveness = "Effective" },
                new RiskControl { ControlName = "System Monitoring", ControlType = "Detective", Effectiveness = "Effective" }
            }
        });
    }

    public Task<OperationalRisk?> GetOperationalRiskAsync(string id)
        => Task.FromResult(_risks.FirstOrDefault(r => r.Id == id));

    public Task<List<OperationalRisk>> GetAllOperationalRisksAsync()
        => Task.FromResult(_risks.ToList());

    public Task<List<OperationalRisk>> GetRisksByCategory(string category)
        => Task.FromResult(_risks.Where(r => r.RiskCategory == category).ToList());

    public Task<OperationalRisk> CreateOperationalRiskAsync(OperationalRisk risk)
    {
        risk.Id = Guid.NewGuid().ToString();
        risk.AssessmentDate = DateTime.UtcNow;
        _risks.Add(risk);
        return Task.FromResult(risk);
    }

    public Task<OperationalRisk> UpdateOperationalRiskAsync(OperationalRisk risk)
    {
        var existing = _risks.FindIndex(r => r.Id == risk.Id);
        if (existing >= 0)
            _risks[existing] = risk;
        return Task.FromResult(risk);
    }

    public Task<OperationalRisk> AddControlAsync(string riskId, RiskControl control)
    {
        var risk = _risks.FirstOrDefault(r => r.Id == riskId);
        if (risk != null)
        {
            control.Id = Guid.NewGuid().ToString();
            risk.Controls.Add(control);
        }
        return Task.FromResult(risk ?? throw new InvalidOperationException("Risk not found"));
    }

    public Task<OperationalRisk> UpdateControlEffectivenessAsync(string riskId, string controlId, string effectiveness)
    {
        var risk = _risks.FirstOrDefault(r => r.Id == riskId);
        var control = risk?.Controls.FirstOrDefault(c => c.Id == controlId);
        if (control != null)
        {
            control.Effectiveness = effectiveness;
            control.LastTestedDate = DateTime.UtcNow;
        }
        return Task.FromResult(risk ?? throw new InvalidOperationException("Risk not found"));
    }

    public Task<List<KeyRiskIndicator>> GetKeyRiskIndicatorsAsync()
    {
        return Task.FromResult(new List<KeyRiskIndicator>
        {
            new KeyRiskIndicator { IndicatorName = "System Uptime", CurrentValue = 99.95m, Threshold = 99.5m, ThresholdType = "Min", IsBreached = false, Trend = "Stable" },
            new KeyRiskIndicator { IndicatorName = "Failed Trades", CurrentValue = 0.02m, Threshold = 0.1m, ThresholdType = "Max", IsBreached = false, Trend = "Improving" },
            new KeyRiskIndicator { IndicatorName = "Settlement Fails", CurrentValue = 0.5m, Threshold = 1m, ThresholdType = "Max", IsBreached = false, Trend = "Stable" }
        });
    }

    public Task<List<OperationalRisk>> GetHighRiskItemsAsync()
        => Task.FromResult(_risks.Where(r => r.RiskLevel == "High" || r.RiskLevel == "Critical").ToList());

    public Task LinkIncidentToRiskAsync(string riskId, string incidentId)
    {
        var risk = _risks.FirstOrDefault(r => r.Id == riskId);
        if (risk != null)
        {
            risk.RelatedIncidentIds.Add(incidentId);
            risk.IncidentCount12Months++;
        }
        return Task.CompletedTask;
    }
}

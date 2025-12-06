using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Risk;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IRiskService for development and testing.
/// </summary>
public class MockRiskService : IRiskService
{
    private readonly List<VaRCalculation> _varCalculations = new();
    private readonly List<StressTest> _stressTests = new();
    private readonly List<StressScenarioTemplate> _scenarioTemplates = new();
    private readonly List<ExposureMonitoring> _exposures = new();
    private readonly List<MarketSurveillance> _surveillanceAlerts = new();
    private readonly List<CircuitBreaker> _circuitBreakers = new();

    public MockRiskService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed VaR calculation
        _varCalculations.Add(new VaRCalculation
        {
            CalculationDate = DateTime.UtcNow,
            AsOfDate = DateTime.UtcNow.Date,
            EntityId = "CM-001",
            EntityName = "Zambia Mining Corp",
            EntityType = "ClearingMember",
            ConfidenceLevel = 99m,
            HoldingPeriod = 1,
            CalculationMethod = "HistoricalSimulation",
            HistoricalDays = 250,
            VaRAmount = 750000,
            Currency = "USD",
            VaRAsPercentageOfPortfolio = 5m,
            ExpectedShortfall = 950000,
            ExpectedShortfallPercentage = 6.3m,
            PortfolioValue = 15000000,
            VaRLimit = 1000000,
            StressVaR = 1200000,
            StressScenarioApplied = "2008 Financial Crisis",
            Contributions = new List<VaRContribution>
            {
                new VaRContribution
                {
                    PositionId = "pos-001",
                    MetalType = MetalType.Copper,
                    PositionType = "Long",
                    Quantity = 500,
                    MarketValue = 4262500,
                    MarginalVaR = 35000,
                    ComponentVaR = 280000,
                    PercentageContribution = 37.3m,
                    Volatility = 0.22m,
                    Beta = 1.1m
                },
                new VaRContribution
                {
                    PositionId = "pos-002",
                    MetalType = MetalType.Cobalt,
                    PositionType = "Long",
                    Quantity = 100,
                    MarketValue = 2800000,
                    MarginalVaR = 45000,
                    ComponentVaR = 350000,
                    PercentageContribution = 46.7m,
                    Volatility = 0.35m,
                    Beta = 1.3m
                }
            }
        });

        // Seed stress test
        _stressTests.Add(new StressTest
        {
            TestName = "Copper Price Crash Scenario",
            ExecutionDate = DateTime.UtcNow.AddDays(-7),
            TestType = "Historical",
            Scope = "Exchange",
            ScenarioName = "2008 Commodity Crash",
            ScenarioDescription = "Simulates the 2008 financial crisis impact on copper prices",
            Parameters = new List<StressScenarioParameter>
            {
                new StressScenarioParameter
                {
                    ParameterName = "CopperPrice",
                    ParameterType = "PriceShock",
                    MetalType = MetalType.Copper,
                    BaseValue = 8525,
                    ShockValue = 5115,
                    ShockPercentage = 40,
                    ShockDirection = "Down"
                }
            },
            BaselineValue = 100000000,
            StressedValue = 65000000,
            Loss = 35000000,
            LossPercentage = 35,
            Currency = "USD",
            GuaranteeFundCoverage = 10000000,
            GuaranteeFundSufficient = false,
            AdditionalCapitalNeeded = 25000000,
            HistoricalEventName = "2008 Financial Crisis",
            HistoricalEventDate = new DateTime(2008, 10, 1),
            RiskLevel = "High",
            RecommendedActions = new List<string>
            {
                "Increase guarantee fund contributions",
                "Review position limits for high-risk members",
                "Implement stricter margin requirements"
            },
            ApprovedBy = "Chief Risk Officer",
            ApprovalDate = DateTime.UtcNow.AddDays(-5)
        });

        // Seed scenario templates
        _scenarioTemplates.AddRange(new[]
        {
            new StressScenarioTemplate
            {
                Name = "Copper 40% Price Drop",
                Description = "Historical simulation of 2008 copper price crash",
                ScenarioType = "Historical",
                IsActive = true,
                Parameters = new List<StressScenarioParameter>
                {
                    new StressScenarioParameter
                    {
                        ParameterName = "CopperPrice",
                        ParameterType = "PriceShock",
                        MetalType = MetalType.Copper,
                        ShockPercentage = 40,
                        ShockDirection = "Down"
                    }
                },
                CreatedBy = "Risk Team"
            },
            new StressScenarioTemplate
            {
                Name = "Cobalt Supply Shock",
                Description = "Hypothetical DRC supply disruption scenario",
                ScenarioType = "Hypothetical",
                IsActive = true,
                Parameters = new List<StressScenarioParameter>
                {
                    new StressScenarioParameter
                    {
                        ParameterName = "CobaltPrice",
                        ParameterType = "PriceShock",
                        MetalType = MetalType.Cobalt,
                        ShockPercentage = 50,
                        ShockDirection = "Up"
                    },
                    new StressScenarioParameter
                    {
                        ParameterName = "CobaltVolatility",
                        ParameterType = "VolatilityShock",
                        MetalType = MetalType.Cobalt,
                        ShockPercentage = 100,
                        ShockDirection = "Up"
                    }
                },
                CreatedBy = "Risk Team"
            }
        });

        // Seed exposure monitoring
        _exposures.Add(new ExposureMonitoring
        {
            AsOfDate = DateTime.UtcNow,
            EntityId = "CM-001",
            EntityName = "Zambia Mining Corp",
            EntityType = "ClearingMember",
            GrossExposure = 15000000,
            NetExposure = 5000000,
            Currency = "USD",
            GrossExposureLimit = 50000000,
            NetExposureLimit = 25000000,
            AlertLevel = "Normal",
            CollateralHeld = 2000000,
            CollateralRequired = 1500000,
            ExposureChange24h = 500000,
            ExposureChangePercentage24h = 3.4m,
            ExposureDetails = new List<ExposureDetail>
            {
                new ExposureDetail
                {
                    PositionId = "pos-001",
                    TradeId = "trade-001",
                    MetalType = MetalType.Copper,
                    Side = "Long",
                    Quantity = 500,
                    MarketPrice = 8525,
                    MarketValue = 4262500,
                    Exposure = 4262500,
                    TradeDate = DateTime.Now.AddDays(-5),
                    SettlementDate = DateTime.Now.AddDays(-3),
                    DaysToSettlement = 0
                }
            }
        });

        // Seed circuit breakers
        foreach (MetalType metal in Enum.GetValues(typeof(MetalType)))
        {
            _circuitBreakers.Add(new CircuitBreaker
            {
                MetalType = metal,
                PriceMovementThreshold = 10, // 10%
                TimeWindowMinutes = 5,
                HaltDurationMinutes = 15,
                Status = "Active",
                IsTriggered = false
            });
        }
    }

    // VaR Calculations
    public Task<IEnumerable<VaRCalculation>> GetAllVaRCalculationsAsync()
        => Task.FromResult<IEnumerable<VaRCalculation>>(_varCalculations);

    public Task<VaRCalculation?> GetVaRCalculationByIdAsync(string id)
        => Task.FromResult(_varCalculations.FirstOrDefault(v => v.Id == id));

    public Task<VaRCalculation?> GetLatestVaRForEntityAsync(string entityId)
        => Task.FromResult(_varCalculations
            .Where(v => v.EntityId == entityId)
            .OrderByDescending(v => v.CalculationDate)
            .FirstOrDefault());

    public Task<VaRCalculation> CalculateVaRAsync(string entityId, string entityType, decimal confidenceLevel = 99m, int holdingPeriod = 1)
    {
        var var = new VaRCalculation
        {
            CalculationDate = DateTime.UtcNow,
            AsOfDate = DateTime.UtcNow.Date,
            EntityId = entityId,
            EntityType = entityType,
            ConfidenceLevel = confidenceLevel,
            HoldingPeriod = holdingPeriod,
            CalculationMethod = "HistoricalSimulation",
            VaRAmount = new Random().Next(100000, 1000000),
            Currency = "USD",
            PortfolioValue = new Random().Next(5000000, 20000000),
            VaRLimit = 1000000
        };
        var.VaRAsPercentageOfPortfolio = (var.VaRAmount / var.PortfolioValue) * 100;
        var.ExpectedShortfall = var.VaRAmount * 1.25m;
        _varCalculations.Add(var);
        return Task.FromResult(var);
    }

    public Task<IEnumerable<VaRCalculation>> GetVaRHistoryAsync(string entityId, DateTime fromDate, DateTime toDate)
        => Task.FromResult<IEnumerable<VaRCalculation>>(
            _varCalculations.Where(v => v.EntityId == entityId && v.AsOfDate >= fromDate && v.AsOfDate <= toDate));

    public Task<IEnumerable<VaRCalculation>> GetVaRBreachesAsync()
        => Task.FromResult<IEnumerable<VaRCalculation>>(_varCalculations.Where(v => v.ExceedsLimit));

    // Stress Testing
    public Task<IEnumerable<StressTest>> GetAllStressTestsAsync()
        => Task.FromResult<IEnumerable<StressTest>>(_stressTests);

    public Task<StressTest?> GetStressTestByIdAsync(string id)
        => Task.FromResult(_stressTests.FirstOrDefault(s => s.Id == id));

    public Task<IEnumerable<StressTest>> GetStressTestsByTypeAsync(string testType)
        => Task.FromResult<IEnumerable<StressTest>>(_stressTests.Where(s => s.TestType == testType));

    public Task<StressTest> ExecuteStressTestAsync(string scenarioId, string? entityId = null)
    {
        var template = _scenarioTemplates.FirstOrDefault(t => t.Id == scenarioId);
        var stressTest = new StressTest
        {
            TestName = template?.Name ?? "Custom Stress Test",
            ExecutionDate = DateTime.UtcNow,
            TestType = template?.ScenarioType ?? "Hypothetical",
            Scope = entityId != null ? "ClearingMember" : "Exchange",
            EntityId = entityId,
            ScenarioName = template?.Name ?? "Custom Scenario",
            ScenarioDescription = template?.Description ?? "",
            Parameters = template?.Parameters ?? new List<StressScenarioParameter>(),
            BaselineValue = 100000000,
            StressedValue = 75000000,
            Loss = 25000000,
            LossPercentage = 25,
            Currency = "USD",
            RiskLevel = "Moderate"
        };
        _stressTests.Add(stressTest);
        return Task.FromResult(stressTest);
    }

    public Task<StressTest> ExecuteHistoricalScenarioAsync(string scenarioName, DateTime historicalDate)
    {
        var stressTest = new StressTest
        {
            TestName = scenarioName,
            ExecutionDate = DateTime.UtcNow,
            TestType = "Historical",
            Scope = "Exchange",
            ScenarioName = scenarioName,
            HistoricalEventDate = historicalDate,
            BaselineValue = 100000000,
            StressedValue = 70000000,
            Loss = 30000000,
            LossPercentage = 30,
            Currency = "USD",
            RiskLevel = "High"
        };
        _stressTests.Add(stressTest);
        return Task.FromResult(stressTest);
    }

    public Task<StressTest> ExecuteHypotheticalScenarioAsync(string scenarioName, List<StressScenarioParameter> parameters)
    {
        var stressTest = new StressTest
        {
            TestName = scenarioName,
            ExecutionDate = DateTime.UtcNow,
            TestType = "Hypothetical",
            Scope = "Exchange",
            ScenarioName = scenarioName,
            Parameters = parameters,
            BaselineValue = 100000000,
            StressedValue = 80000000,
            Loss = 20000000,
            LossPercentage = 20,
            Currency = "USD",
            RiskLevel = "Moderate"
        };
        _stressTests.Add(stressTest);
        return Task.FromResult(stressTest);
    }

    // Stress Scenario Templates
    public Task<IEnumerable<StressScenarioTemplate>> GetStressScenarioTemplatesAsync()
        => Task.FromResult<IEnumerable<StressScenarioTemplate>>(_scenarioTemplates.Where(t => t.IsActive));

    public Task<StressScenarioTemplate?> GetStressScenarioTemplateByIdAsync(string id)
        => Task.FromResult(_scenarioTemplates.FirstOrDefault(t => t.Id == id));

    public Task<StressScenarioTemplate> CreateStressScenarioTemplateAsync(StressScenarioTemplate template)
    {
        template.Id = Guid.NewGuid().ToString();
        template.CreatedDate = DateTime.UtcNow;
        _scenarioTemplates.Add(template);
        return Task.FromResult(template);
    }

    public Task<StressScenarioTemplate> UpdateStressScenarioTemplateAsync(StressScenarioTemplate template)
    {
        var existing = _scenarioTemplates.FirstOrDefault(t => t.Id == template.Id);
        if (existing != null)
        {
            _scenarioTemplates.Remove(existing);
            _scenarioTemplates.Add(template);
        }
        return Task.FromResult(template);
    }

    // Exposure Monitoring
    public Task<IEnumerable<ExposureMonitoring>> GetAllExposuresAsync()
        => Task.FromResult<IEnumerable<ExposureMonitoring>>(_exposures);

    public Task<ExposureMonitoring?> GetExposureByIdAsync(string id)
        => Task.FromResult(_exposures.FirstOrDefault(e => e.Id == id));

    public Task<ExposureMonitoring?> GetExposureByEntityAsync(string entityId)
        => Task.FromResult(_exposures.FirstOrDefault(e => e.EntityId == entityId));

    public Task<ExposureMonitoring> CalculateExposureAsync(string entityId)
    {
        var exposure = _exposures.FirstOrDefault(e => e.EntityId == entityId);
        if (exposure == null)
        {
            exposure = new ExposureMonitoring
            {
                EntityId = entityId,
                AsOfDate = DateTime.UtcNow,
                GrossExposure = new Random().Next(1000000, 20000000),
                NetExposure = new Random().Next(-5000000, 5000000),
                Currency = "USD",
                GrossExposureLimit = 50000000,
                NetExposureLimit = 25000000,
                AlertLevel = "Normal"
            };
            _exposures.Add(exposure);
        }
        else
        {
            exposure.AsOfDate = DateTime.UtcNow;
        }
        return Task.FromResult(exposure);
    }

    public Task<IEnumerable<ExposureMonitoring>> GetExposureBreachesAsync()
        => Task.FromResult<IEnumerable<ExposureMonitoring>>(_exposures.Where(e => e.IsInBreach));

    public Task<IEnumerable<ExposureMonitoring>> GetExposureWarningsAsync(decimal thresholdPercentage = 80m)
        => Task.FromResult<IEnumerable<ExposureMonitoring>>(
            _exposures.Where(e => e.GrossUtilization >= thresholdPercentage || e.NetUtilization >= thresholdPercentage));

    public Task<ExposureAlert> CreateExposureAlertAsync(string entityId, string alertType, string severity, string message)
    {
        var exposure = _exposures.FirstOrDefault(e => e.EntityId == entityId);
        var alert = new ExposureAlert
        {
            AlertTime = DateTime.UtcNow,
            AlertType = alertType,
            Severity = severity,
            Message = message
        };
        if (exposure != null)
        {
            exposure.Alerts.Add(alert);
            exposure.AlertLevel = severity == "Critical" ? "Critical" : severity == "Warning" ? "Warning" : exposure.AlertLevel;
        }
        return Task.FromResult(alert);
    }

    public Task<ExposureAlert> AcknowledgeAlertAsync(string alertId, string acknowledgedBy)
    {
        var alert = _exposures.SelectMany(e => e.Alerts).FirstOrDefault(a => a.Id == alertId);
        if (alert != null)
        {
            alert.Acknowledged = true;
            alert.AcknowledgedTime = DateTime.UtcNow;
            alert.AcknowledgedBy = acknowledgedBy;
        }
        return Task.FromResult(alert!);
    }

    // Market Surveillance
    public Task<IEnumerable<MarketSurveillance>> GetAllSurveillanceAlertsAsync()
        => Task.FromResult<IEnumerable<MarketSurveillance>>(_surveillanceAlerts);

    public Task<MarketSurveillance?> GetSurveillanceAlertByIdAsync(string id)
        => Task.FromResult(_surveillanceAlerts.FirstOrDefault(s => s.Id == id));

    public Task<IEnumerable<MarketSurveillance>> GetSurveillanceAlertsByTypeAsync(string alertType)
        => Task.FromResult<IEnumerable<MarketSurveillance>>(_surveillanceAlerts.Where(s => s.AlertType == alertType));

    public Task<IEnumerable<MarketSurveillance>> GetSurveillanceAlertsByStatusAsync(string status)
        => Task.FromResult<IEnumerable<MarketSurveillance>>(_surveillanceAlerts.Where(s => s.Status == status));

    public Task<MarketSurveillance> CreateSurveillanceAlertAsync(MarketSurveillance alert)
    {
        alert.Id = Guid.NewGuid().ToString();
        alert.DetectionTime = DateTime.UtcNow;
        alert.Status = "Detected";
        _surveillanceAlerts.Add(alert);
        return Task.FromResult(alert);
    }

    public Task<MarketSurveillance> UpdateSurveillanceAlertStatusAsync(string id, string status, string? findings = null)
    {
        var alert = _surveillanceAlerts.FirstOrDefault(s => s.Id == id);
        if (alert != null)
        {
            alert.Status = status;
            if (findings != null) alert.Findings = findings;
            if (status == "Closed") alert.ResolutionDate = DateTime.UtcNow;
        }
        return Task.FromResult(alert!);
    }

    public Task<MarketSurveillance> AssignSurveillanceAlertAsync(string id, string assignedTo)
    {
        var alert = _surveillanceAlerts.FirstOrDefault(s => s.Id == id);
        if (alert != null)
        {
            alert.AssignedTo = assignedTo;
            alert.InvestigationStartDate = DateTime.UtcNow;
            alert.Status = "UnderReview";
        }
        return Task.FromResult(alert!);
    }

    public Task<MarketSurveillance> ReportToRegulatorAsync(string id)
    {
        var alert = _surveillanceAlerts.FirstOrDefault(s => s.Id == id);
        if (alert != null)
        {
            alert.ReportedToRegulator = true;
            alert.ReportedDate = DateTime.UtcNow;
            alert.RegulatoryReportReference = $"SEC-ZM-{DateTime.Now:yyyyMMdd}-{new Random().Next(1000, 9999)}";
        }
        return Task.FromResult(alert!);
    }

    // Circuit Breakers
    public Task<IEnumerable<CircuitBreaker>> GetAllCircuitBreakersAsync()
        => Task.FromResult<IEnumerable<CircuitBreaker>>(_circuitBreakers);

    public Task<CircuitBreaker?> GetCircuitBreakerByMetalAsync(MetalType metalType)
        => Task.FromResult(_circuitBreakers.FirstOrDefault(c => c.MetalType == metalType));

    public Task<CircuitBreaker> TriggerCircuitBreakerAsync(MetalType metalType, decimal triggerPrice, decimal referencePrice, string reason)
    {
        var cb = _circuitBreakers.FirstOrDefault(c => c.MetalType == metalType);
        if (cb != null)
        {
            cb.IsTriggered = true;
            cb.Status = "Triggered";
            cb.TriggeredTime = DateTime.UtcNow;
            cb.TriggeringPrice = triggerPrice;
            cb.ReferencePrice = referencePrice;
            cb.PriceMovement = ((triggerPrice - referencePrice) / referencePrice) * 100;
            cb.TriggerReason = reason;
            cb.ResumeTime = DateTime.UtcNow.AddMinutes(cb.HaltDurationMinutes);
        }
        return Task.FromResult(cb!);
    }

    public Task<CircuitBreaker> ResetCircuitBreakerAsync(MetalType metalType)
    {
        var cb = _circuitBreakers.FirstOrDefault(c => c.MetalType == metalType);
        if (cb != null)
        {
            cb.IsTriggered = false;
            cb.Status = "Active";
            cb.TriggeredTime = null;
            cb.ResumeTime = null;
            cb.TriggeringPrice = null;
            cb.ReferencePrice = null;
            cb.PriceMovement = null;
            cb.TriggerReason = null;
        }
        return Task.FromResult(cb!);
    }

    public Task<CircuitBreaker> UpdateCircuitBreakerConfigAsync(MetalType metalType, decimal threshold, int haltDuration)
    {
        var cb = _circuitBreakers.FirstOrDefault(c => c.MetalType == metalType);
        if (cb != null)
        {
            cb.PriceMovementThreshold = threshold;
            cb.HaltDurationMinutes = haltDuration;
        }
        return Task.FromResult(cb!);
    }

    // Anomaly Detection
    public Task<IEnumerable<MarketSurveillance>> DetectPriceAnomaliesAsync(MetalType metalType, DateTime fromTime, DateTime toTime)
    {
        // Mock implementation - in production would analyze price data
        return Task.FromResult<IEnumerable<MarketSurveillance>>(
            _surveillanceAlerts.Where(s => s.MetalType == metalType && s.AlertType.Contains("Price")));
    }

    public Task<IEnumerable<MarketSurveillance>> DetectVolumeAnomaliesAsync(MetalType metalType, DateTime fromTime, DateTime toTime)
    {
        return Task.FromResult<IEnumerable<MarketSurveillance>>(
            _surveillanceAlerts.Where(s => s.MetalType == metalType && s.VolumeAnomaly > 0));
    }

    public Task<IEnumerable<MarketSurveillance>> DetectPatternAnomaliesAsync(string participantId)
    {
        return Task.FromResult<IEnumerable<MarketSurveillance>>(
            _surveillanceAlerts.Where(s => s.SuspectedParties.Any(p => p.PartyId == participantId)));
    }
}

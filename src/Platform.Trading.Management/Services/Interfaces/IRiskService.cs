using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Risk;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for risk management operations.
/// Addresses Gaps RM-001, RM-002, RM-004, TO-003: VaR, stress testing, exposure monitoring, market surveillance.
/// </summary>
public interface IRiskService
{
    // VaR Calculations
    Task<IEnumerable<VaRCalculation>> GetAllVaRCalculationsAsync();
    Task<VaRCalculation?> GetVaRCalculationByIdAsync(string id);
    Task<VaRCalculation?> GetLatestVaRForEntityAsync(string entityId);
    Task<VaRCalculation> CalculateVaRAsync(string entityId, string entityType, decimal confidenceLevel = 99m, int holdingPeriod = 1);
    Task<IEnumerable<VaRCalculation>> GetVaRHistoryAsync(string entityId, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<VaRCalculation>> GetVaRBreachesAsync();
    
    // Stress Testing
    Task<IEnumerable<StressTest>> GetAllStressTestsAsync();
    Task<StressTest?> GetStressTestByIdAsync(string id);
    Task<IEnumerable<StressTest>> GetStressTestsByTypeAsync(string testType);
    Task<StressTest> ExecuteStressTestAsync(string scenarioId, string? entityId = null);
    Task<StressTest> ExecuteHistoricalScenarioAsync(string scenarioName, DateTime historicalDate);
    Task<StressTest> ExecuteHypotheticalScenarioAsync(string scenarioName, List<StressScenarioParameter> parameters);
    
    // Stress Scenario Templates
    Task<IEnumerable<StressScenarioTemplate>> GetStressScenarioTemplatesAsync();
    Task<StressScenarioTemplate?> GetStressScenarioTemplateByIdAsync(string id);
    Task<StressScenarioTemplate> CreateStressScenarioTemplateAsync(StressScenarioTemplate template);
    Task<StressScenarioTemplate> UpdateStressScenarioTemplateAsync(StressScenarioTemplate template);
    
    // Exposure Monitoring
    Task<IEnumerable<ExposureMonitoring>> GetAllExposuresAsync();
    Task<ExposureMonitoring?> GetExposureByIdAsync(string id);
    Task<ExposureMonitoring?> GetExposureByEntityAsync(string entityId);
    Task<ExposureMonitoring> CalculateExposureAsync(string entityId);
    Task<IEnumerable<ExposureMonitoring>> GetExposureBreachesAsync();
    Task<IEnumerable<ExposureMonitoring>> GetExposureWarningsAsync(decimal thresholdPercentage = 80m);
    Task<ExposureAlert> CreateExposureAlertAsync(string entityId, string alertType, string severity, string message);
    Task<ExposureAlert> AcknowledgeAlertAsync(string alertId, string acknowledgedBy);
    
    // Market Surveillance
    Task<IEnumerable<MarketSurveillance>> GetAllSurveillanceAlertsAsync();
    Task<MarketSurveillance?> GetSurveillanceAlertByIdAsync(string id);
    Task<IEnumerable<MarketSurveillance>> GetSurveillanceAlertsByTypeAsync(string alertType);
    Task<IEnumerable<MarketSurveillance>> GetSurveillanceAlertsByStatusAsync(string status);
    Task<MarketSurveillance> CreateSurveillanceAlertAsync(MarketSurveillance alert);
    Task<MarketSurveillance> UpdateSurveillanceAlertStatusAsync(string id, string status, string? findings = null);
    Task<MarketSurveillance> AssignSurveillanceAlertAsync(string id, string assignedTo);
    Task<MarketSurveillance> ReportToRegulatorAsync(string id);
    
    // Circuit Breakers
    Task<IEnumerable<CircuitBreaker>> GetAllCircuitBreakersAsync();
    Task<CircuitBreaker?> GetCircuitBreakerByMetalAsync(MetalType metalType);
    Task<CircuitBreaker> TriggerCircuitBreakerAsync(MetalType metalType, decimal triggerPrice, decimal referencePrice, string reason);
    Task<CircuitBreaker> ResetCircuitBreakerAsync(MetalType metalType);
    Task<CircuitBreaker> UpdateCircuitBreakerConfigAsync(MetalType metalType, decimal threshold, int haltDuration);
    
    // Anomaly Detection
    Task<IEnumerable<MarketSurveillance>> DetectPriceAnomaliesAsync(MetalType metalType, DateTime fromTime, DateTime toTime);
    Task<IEnumerable<MarketSurveillance>> DetectVolumeAnomaliesAsync(MetalType metalType, DateTime fromTime, DateTime toTime);
    Task<IEnumerable<MarketSurveillance>> DetectPatternAnomaliesAsync(string participantId);
}

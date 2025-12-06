using Platform.Trading.Management.Models.Risk;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Interface for credit risk scoring service.
/// Addresses Gap RM-003: Automated credit scoring for counterparties.
/// </summary>
public interface ICreditRiskService
{
    Task<CreditRiskScore?> GetCreditScoreAsync(string entityId);
    Task<List<CreditRiskScore>> GetAllCreditScoresAsync();
    Task<CreditRiskScore> CalculateCreditScoreAsync(string entityId);
    Task<CreditRiskScore> UpdateCreditScoreAsync(CreditRiskScore score);
    Task<decimal> GetRecommendedCreditLimitAsync(string entityId);
    Task<List<CreditRiskScore>> GetHighRiskEntitiesAsync();
    Task<List<CreditAlert>> GetActiveCreditAlertsAsync(string? entityId = null);
    Task ScheduleCreditReviewAsync(string entityId, DateTime reviewDate);
}

/// <summary>
/// Interface for liquidity risk management service.
/// Addresses Gap RM-005: Intraday liquidity monitoring and stress testing.
/// </summary>
public interface ILiquidityRiskService
{
    Task<LiquidityRisk?> GetLiquidityRiskAsync(string entityId);
    Task<List<LiquidityRisk>> GetAllLiquidityRisksAsync();
    Task<LiquidityRisk> CalculateLiquidityRiskAsync(string entityId);
    Task<LiquidityRisk> RunIntradayMonitoringAsync(string entityId);
    Task<List<LiquidityStressScenario>> RunLiquidityStressTestAsync(string entityId);
    Task<List<LiquidityAlert>> GetLiquidityAlertsAsync();
    Task ActivateContingencyPlanAsync(string entityId, string measure);
    Task<List<LiquiditySource>> GetLiquiditySourcesAsync(string entityId);
}

/// <summary>
/// Interface for concentration limit service.
/// Addresses Gap RM-010: Sector and counterparty concentration limits.
/// </summary>
public interface IConcentrationLimitService
{
    Task<ConcentrationLimit?> GetConcentrationLimitAsync(string id);
    Task<List<ConcentrationLimit>> GetAllConcentrationLimitsAsync();
    Task<List<ConcentrationLimit>> GetLimitsByTypeAsync(string limitType);
    Task<ConcentrationLimit> CreateConcentrationLimitAsync(ConcentrationLimit limit);
    Task<ConcentrationLimit> UpdateConcentrationLimitAsync(ConcentrationLimit limit);
    Task<List<ConcentrationLimit>> CheckAllConcentrationsAsync();
    Task<List<ConcentrationLimit>> GetBreachedLimitsAsync();
    Task<ConcentrationLimit> GrantExemptionAsync(string limitId, string reason, DateTime expiryDate, string approvedBy);
    Task<decimal> CalculateCurrentConcentrationAsync(string limitType, string? entityId = null);
}

/// <summary>
/// Interface for operational risk service.
/// Addresses Gap RM-006: Comprehensive operational risk management.
/// </summary>
public interface IOperationalRiskService
{
    Task<OperationalRisk?> GetOperationalRiskAsync(string id);
    Task<List<OperationalRisk>> GetAllOperationalRisksAsync();
    Task<List<OperationalRisk>> GetRisksByCategory(string category);
    Task<OperationalRisk> CreateOperationalRiskAsync(OperationalRisk risk);
    Task<OperationalRisk> UpdateOperationalRiskAsync(OperationalRisk risk);
    Task<OperationalRisk> AddControlAsync(string riskId, RiskControl control);
    Task<OperationalRisk> UpdateControlEffectivenessAsync(string riskId, string controlId, string effectiveness);
    Task<List<KeyRiskIndicator>> GetKeyRiskIndicatorsAsync();
    Task<List<OperationalRisk>> GetHighRiskItemsAsync();
    Task LinkIncidentToRiskAsync(string riskId, string incidentId);
}

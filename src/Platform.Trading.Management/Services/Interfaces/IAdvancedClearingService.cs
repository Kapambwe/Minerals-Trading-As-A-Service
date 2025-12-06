using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Clearing;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Interface for multilateral netting service.
/// Addresses Gap CS-004: Multilateral netting to reduce settlement volumes.
/// </summary>
public interface INettingService
{
    Task<NettingCalculation?> GetNettingCalculationAsync(string id);
    Task<List<NettingCalculation>> GetNettingCyclesAsync(DateTime settlementDate);
    Task<NettingCalculation> CalculateNettingAsync(DateTime settlementDate, string nettingType);
    Task<NettingCalculation> ApplyNettingAsync(string calculationId, string approvedBy);
    Task<decimal> GetNettingEfficiencyAsync(DateTime fromDate, DateTime toDate);
}

/// <summary>
/// Interface for collateral management service.
/// Addresses Gap CS-007: Sophisticated collateral eligibility and haircut management.
/// </summary>
public interface ICollateralService
{
    Task<CollateralManagement?> GetCollateralAccountAsync(string participantId);
    Task<List<CollateralManagement>> GetAllCollateralAccountsAsync();
    Task<CollateralHolding> PledgeCollateralAsync(string participantId, CollateralHolding holding);
    Task<CollateralHolding> ReleaseCollateralAsync(string participantId, string holdingId);
    Task<CollateralHolding> SubstituteCollateralAsync(string participantId, string oldHoldingId, CollateralHolding newHolding);
    Task<decimal> CalculateHaircutAsync(CollateralHolding holding);
    Task<bool> CheckEligibilityAsync(CollateralHolding holding);
    Task<List<ConcentrationCheck>> CheckConcentrationLimitsAsync(string participantId);
    Task RevaluateCollateralAsync(string participantId);
    Task<List<CollateralEligibility>> GetEligibilityCriteriaAsync();
}

/// <summary>
/// Interface for position limit management service.
/// Addresses Gap CS-008: Configurable position limits per participant.
/// </summary>
public interface IPositionLimitService
{
    Task<PositionLimit?> GetPositionLimitAsync(string participantId, MetalType? metalType = null);
    Task<List<PositionLimit>> GetAllPositionLimitsAsync();
    Task<PositionLimit> CreatePositionLimitAsync(PositionLimit limit);
    Task<PositionLimit> UpdatePositionLimitAsync(PositionLimit limit);
    Task<bool> CheckPositionLimitAsync(string participantId, MetalType metalType, decimal quantity, string side);
    Task<List<PositionLimit>> GetBreachedLimitsAsync();
    Task<PositionLimit> GrantExemptionAsync(string limitId, string reason, DateTime expiryDate, string approvedBy);
}

/// <summary>
/// Interface for mark-to-market service.
/// Addresses Gap CS-009: Automated daily mark-to-market with margin calls.
/// </summary>
public interface IMarkToMarketService
{
    Task<MarkToMarket?> GetMtmAsync(string entityId, DateTime valuationDate);
    Task<List<MarkToMarket>> GetAllMtmAsync(DateTime valuationDate);
    Task<MarkToMarket> CalculateMtmAsync(string entityId);
    Task RunEndOfDayMtmAsync();
    Task RunIntradayMtmAsync();
    Task<List<MarkToMarket>> GetMarginCallsAsync();
    Task ProcessMarginCallAsync(string mtmId);
    Task ProcessVariationMarginAsync(string mtmId);
}

/// <summary>
/// Interface for interest calculation service.
/// Addresses Gap CS-010: Interest on margin balances and late payments.
/// </summary>
public interface IInterestService
{
    Task<InterestCalculation?> GetInterestCalculationAsync(string id);
    Task<List<InterestCalculation>> GetInterestCalculationsAsync(string participantId, string period);
    Task<List<InterestCalculation>> CalculateMonthlyInterestAsync(string period);
    Task ApplyInterestAsync(string calculationId);
    Task<decimal> GetCurrentInterestRateAsync(string balanceType);
}

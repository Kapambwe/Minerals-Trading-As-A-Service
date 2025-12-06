using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Clearing;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for Central Counterparty (CCP) and clearing operations.
/// Addresses Gaps CS-001, CS-002, CS-003, CS-006: CCP infrastructure, RTGS, DvP, and default management.
/// </summary>
public interface IClearingService
{
    // Clearing Members
    Task<IEnumerable<ClearingMember>> GetAllClearingMembersAsync();
    Task<ClearingMember?> GetClearingMemberByIdAsync(string id);
    Task<IEnumerable<ClearingMember>> GetClearingMembersByStatusAsync(string status);
    Task<ClearingMember> CreateClearingMemberAsync(ClearingMember member);
    Task<ClearingMember> UpdateClearingMemberAsync(ClearingMember member);
    Task<ClearingMember> SuspendClearingMemberAsync(string id, string reason);
    Task<ClearingMember> ReactivateClearingMemberAsync(string id);
    Task<bool> ValidateMemberCapitalRequirementsAsync(string memberId);
    
    // Guarantee Fund
    Task<GuaranteeFund> GetGuaranteeFundStatusAsync();
    Task<IEnumerable<GuaranteeFundContribution>> GetMemberContributionsAsync();
    Task<GuaranteeFundContribution?> GetMemberContributionAsync(string memberId);
    Task<GuaranteeFundContribution> RecalculateMemberContributionAsync(string memberId);
    Task<GuaranteeFundContribution> RecordContributionPaymentAsync(string memberId, decimal amount, DateTime paymentDate);
    Task<decimal> CalculateTotalGuaranteeFundAsync();
    
    // Default Management
    Task<IEnumerable<DefaultManagement>> GetAllDefaultCasesAsync();
    Task<DefaultManagement?> GetDefaultCaseByIdAsync(string id);
    Task<IEnumerable<DefaultManagement>> GetActiveDefaultCasesAsync();
    Task<DefaultManagement> DeclareDefaultAsync(string memberId, string defaultType, decimal defaultAmount, string description);
    Task<DefaultManagement> UpdateDefaultCaseStatusAsync(string id, string status);
    Task<DefaultManagement> ApplyWaterfallAsync(string caseId);
    Task<DefaultManagement> InitiatePortfolioAuctionAsync(string caseId);
    Task<DefaultManagement> CloseDefaultCaseAsync(string caseId, string resolution);
    Task<List<WaterfallLayer>> GetDefaultWaterfallAsync(string memberId, decimal lossAmount);
    
    // RTGS Integration
    Task<IEnumerable<RtgsTransaction>> GetAllRtgsTransactionsAsync();
    Task<RtgsTransaction?> GetRtgsTransactionByIdAsync(string id);
    Task<IEnumerable<RtgsTransaction>> GetRtgsTransactionsByStatusAsync(string status);
    Task<RtgsTransaction> InitiateRtgsPaymentAsync(RtgsTransaction transaction);
    Task<RtgsTransaction> UpdateRtgsStatusAsync(string id, string status, string? confirmationNumber = null);
    Task<RtgsTransaction?> GetRtgsTransactionByTradeAsync(string tradeId);
    
    // DvP Settlement
    Task<IEnumerable<DvpSettlement>> GetAllDvpSettlementsAsync();
    Task<DvpSettlement?> GetDvpSettlementByIdAsync(string id);
    Task<DvpSettlement?> GetDvpSettlementByTradeAsync(string tradeId);
    Task<IEnumerable<DvpSettlement>> GetDvpSettlementsByStatusAsync(string status);
    Task<DvpSettlement> InitiateDvpSettlementAsync(string tradeId);
    Task<DvpSettlement> BlockDeliveryAsync(string settlementId);
    Task<DvpSettlement> EscrowPaymentAsync(string settlementId);
    Task<DvpSettlement> CompleteDvpSettlementAsync(string settlementId);
    Task<DvpSettlement> RollbackDvpSettlementAsync(string settlementId, string reason);
    
    // Settlement Cycle
    Task<IEnumerable<SettlementCycle>> GetSettlementCyclesAsync(DateTime? fromDate = null, DateTime? toDate = null);
    Task<SettlementCycle> CreateSettlementCycleAsync(DateTime tradeDate, List<string> tradeIds, int settlementDays = 2);
    Task<IEnumerable<SettlementCycle>> GetOverdueSettlementsAsync();
}

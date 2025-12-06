using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Infrastructure;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Interface for real-time data feed service.
/// Addresses Gap TI-004: WebSocket/streaming data for live prices.
/// </summary>
public interface IDataFeedService
{
    Task<DataFeed?> GetDataFeedAsync(string id);
    Task<List<DataFeed>> GetAllDataFeedsAsync();
    Task<DataFeed> CreateDataFeedAsync(DataFeed feed);
    Task<DataFeed> UpdateDataFeedAsync(DataFeed feed);
    Task ConnectAsync(string feedId);
    Task DisconnectAsync(string feedId);
    Task<FeedSubscription> SubscribeAsync(string feedId, FeedSubscription subscription);
    Task UnsubscribeAsync(string feedId, string subscriptionId);
    Task<List<PriceTick>> GetRecentTicksAsync(string source, MetalType metalType, int count = 100);
    Task<DataFeed> GetFeedStatusAsync(string feedId);
    Task<List<DataFeedError>> GetRecentErrorsAsync(string feedId);
}

/// <summary>
/// Interface for membership tier service.
/// Addresses Gap PM-001: Tiered membership with different rights/fees.
/// </summary>
public interface IMembershipTierService
{
    Task<MembershipTier?> GetTierAsync(string tierCode);
    Task<List<MembershipTier>> GetAllTiersAsync();
    Task<MembershipTier> CreateTierAsync(MembershipTier tier);
    Task<MembershipTier> UpdateTierAsync(MembershipTier tier);
    Task<bool> CheckEligibilityAsync(string participantId, string tierCode);
    Task<MembershipTier?> GetParticipantTierAsync(string participantId);
    Task AssignTierAsync(string participantId, string tierCode);
    Task<MembershipFeeStructure> GetFeeStructureAsync(string tierCode);
    Task<decimal> CalculateTradingFeeAsync(string participantId, decimal tradeValue);
}

/// <summary>
/// Interface for onboarding workflow service.
/// Addresses Gap PM-002: Digital onboarding with document management.
/// </summary>
public interface IOnboardingService
{
    Task<OnboardingWorkflow?> GetWorkflowAsync(string applicationNumber);
    Task<List<OnboardingWorkflow>> GetAllWorkflowsAsync();
    Task<List<OnboardingWorkflow>> GetPendingWorkflowsAsync();
    Task<OnboardingWorkflow> CreateWorkflowAsync(OnboardingWorkflow workflow);
    Task<OnboardingWorkflow> UpdateWorkflowAsync(OnboardingWorkflow workflow);
    Task<OnboardingWorkflow> AdvanceStageAsync(string applicationNumber);
    Task<OnboardingWorkflow> SubmitDocumentAsync(string applicationNumber, OnboardingDocument document);
    Task<OnboardingWorkflow> VerifyDocumentAsync(string applicationNumber, string documentId, bool approved, string? rejectionReason = null);
    Task<OnboardingWorkflow> CompleteAmlCheckAsync(string applicationNumber, string result);
    Task<OnboardingWorkflow> CompleteKycCheckAsync(string applicationNumber, string result);
    Task<OnboardingWorkflow> ApproveMembershipAsync(string applicationNumber, string approvedBy);
    Task<OnboardingWorkflow> RejectMembershipAsync(string applicationNumber, string reason);
    Task<OnboardingWorkflow> ActivateMembershipAsync(string applicationNumber);
    Task AssignReviewerAsync(string applicationNumber, string reviewerId);
}

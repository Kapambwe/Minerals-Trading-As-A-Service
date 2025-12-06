using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Participant;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for participant management operations.
/// Addresses Gaps PM-007, PM-010: Dispute resolution and ASM integration.
/// </summary>
public interface IParticipantService
{
    // Artisanal and Small-scale Miners (ASM)
    Task<IEnumerable<ArtisanalMiner>> GetAllArtisanalMinersAsync();
    Task<ArtisanalMiner?> GetArtisanalMinerByIdAsync(string id);
    Task<IEnumerable<ArtisanalMiner>> GetArtisanalMinersByStatusAsync(string status);
    Task<IEnumerable<ArtisanalMiner>> GetArtisanalMinersByProvinceAsync(string province);
    Task<IEnumerable<ArtisanalMiner>> GetArtisanalMinersByAggregationCenterAsync(string centerId);
    Task<ArtisanalMiner> RegisterArtisanalMinerAsync(ArtisanalMiner miner);
    Task<ArtisanalMiner> UpdateArtisanalMinerAsync(ArtisanalMiner miner);
    Task<ArtisanalMiner> ApproveArtisanalMinerAsync(string id, string approvedBy);
    Task<ArtisanalMiner> SuspendArtisanalMinerAsync(string id, string reason);
    Task<AsmProductionRecord> RecordProductionAsync(string minerId, AsmProductionRecord record);
    Task<AsmTrainingRecord> RecordTrainingAsync(string minerId, AsmTrainingRecord record);
    Task<bool> VerifyConflictFreeStatusAsync(string minerId);
    
    // Aggregation Centers
    Task<IEnumerable<AggregationCenter>> GetAllAggregationCentersAsync();
    Task<AggregationCenter?> GetAggregationCenterByIdAsync(string id);
    Task<IEnumerable<AggregationCenter>> GetAggregationCentersByProvinceAsync(string province);
    Task<AggregationCenter> CreateAggregationCenterAsync(AggregationCenter center);
    Task<AggregationCenter> UpdateAggregationCenterAsync(AggregationCenter center);
    Task<IEnumerable<ArtisanalMiner>> GetMinersAtCenterAsync(string centerId);
    
    // Dispute Resolution
    Task<IEnumerable<DisputeCase>> GetAllDisputeCasesAsync();
    Task<DisputeCase?> GetDisputeCaseByIdAsync(string id);
    Task<IEnumerable<DisputeCase>> GetDisputeCasesByStatusAsync(string status);
    Task<IEnumerable<DisputeCase>> GetDisputeCasesByPartyAsync(string partyId);
    Task<DisputeCase> FileDisputeAsync(DisputeCase disputeCase);
    Task<DisputeCase> UpdateDisputeCaseAsync(DisputeCase disputeCase);
    Task<DisputeCase> AssignMediatorAsync(string caseId, string mediatorId, string mediatorName);
    Task<DisputeCase> AssignArbitrationPanelAsync(string caseId, List<string> panelIds, List<string> panelNames);
    Task<DisputeCase> StartMediationAsync(string caseId);
    Task<DisputeCase> StartArbitrationAsync(string caseId);
    Task<DisputeCase> ResolveDisputeAsync(string caseId, string resolutionType, string summary, decimal? awardedAmount = null, string? winningParty = null);
    Task<DisputeCase> WithdrawDisputeAsync(string caseId, string reason);
    Task<DisputeCase> AppealDisputeAsync(string caseId);
    Task<DisputeDocument> AddDocumentAsync(string caseId, DisputeDocument document);
    Task<DisputeEvent> AddEventAsync(string caseId, DisputeEvent @event);
    
    // Auctions
    Task<IEnumerable<MineralAuction>> GetAllAuctionsAsync();
    Task<MineralAuction?> GetAuctionByIdAsync(string id);
    Task<IEnumerable<MineralAuction>> GetAuctionsByStatusAsync(string status);
    Task<IEnumerable<MineralAuction>> GetUpcomingAuctionsAsync();
    Task<MineralAuction> CreateAuctionAsync(MineralAuction auction);
    Task<MineralAuction> UpdateAuctionAsync(MineralAuction auction);
    Task<MineralAuction> OpenAuctionAsync(string auctionId);
    Task<MineralAuction> CloseAuctionAsync(string auctionId);
    Task<MineralAuction> CancelAuctionAsync(string auctionId, string reason);
    Task<MineralAuction> ExtendAuctionAsync(string auctionId, DateTime newEndDate);
    
    // Auction Lots
    Task<AuctionLot> AddLotToAuctionAsync(string auctionId, AuctionLot lot);
    Task<AuctionLot> UpdateLotAsync(string auctionId, AuctionLot lot);
    Task<AuctionLot> WithdrawLotAsync(string auctionId, string lotId);
    Task<IEnumerable<AuctionLot>> GetLotsForAuctionAsync(string auctionId);
    
    // Auction Participants
    Task<AuctionParticipant> RegisterParticipantAsync(string auctionId, AuctionParticipant participant);
    Task<AuctionParticipant> VerifyParticipantAsync(string auctionId, string participantId);
    Task<AuctionParticipant> DisqualifyParticipantAsync(string auctionId, string participantId, string reason);
    
    // Bidding
    Task<AuctionBid> PlaceBidAsync(string auctionId, string lotId, string bidderId, decimal bidAmount);
    Task<IEnumerable<AuctionBid>> GetBidsForLotAsync(string auctionId, string lotId);
    Task<AuctionLot> AwardLotAsync(string auctionId, string lotId, string winningBidderId);
    Task<AuctionLot> MarkLotUnsoldAsync(string auctionId, string lotId);
}

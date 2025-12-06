using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Infrastructure;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of data feed service.
/// </summary>
public class MockDataFeedService : IDataFeedService
{
    private readonly List<DataFeed> _feeds = new();
    private readonly List<PriceTick> _ticks = new();

    public MockDataFeedService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _feeds.AddRange(new[]
        {
            new DataFeed
            {
                Id = Guid.NewGuid().ToString(),
                FeedName = "LME Live Prices",
                FeedType = "Price",
                Protocol = "WebSocket",
                EndpointUrl = "wss://api.lme.com/v1/prices",
                Port = 443,
                UseSSL = true,
                AuthType = "ApiKey",
                RequiresAuthentication = true,
                ConnectionStatus = "Connected",
                LastConnectedTime = DateTime.UtcNow.AddHours(-2),
                LastMessageTime = DateTime.UtcNow.AddSeconds(-5),
                MessagesReceived = 1250000,
                MessagesPerSecond = 45,
                LatencyMs = 15,
                UptimePercentage = 99.95m,
                IsActive = true
            },
            new DataFeed
            {
                Id = Guid.NewGuid().ToString(),
                FeedName = "COMEX Metals",
                FeedType = "Price",
                Protocol = "WebSocket",
                EndpointUrl = "wss://api.cmegroup.com/metals",
                Port = 443,
                UseSSL = true,
                ConnectionStatus = "Connected",
                LastConnectedTime = DateTime.UtcNow.AddHours(-1),
                LastMessageTime = DateTime.UtcNow.AddSeconds(-2),
                MessagesReceived = 890000,
                MessagesPerSecond = 35,
                LatencyMs = 25,
                UptimePercentage = 99.90m,
                IsActive = true
            }
        });

        // Add some mock price ticks
        for (int i = 0; i < 10; i++)
        {
            _ticks.Add(new PriceTick
            {
                Id = Guid.NewGuid().ToString(),
                Timestamp = DateTime.UtcNow.AddSeconds(-i * 5),
                SequenceNumber = 1000 - i,
                Source = "LME",
                MetalType = MetalType.Copper,
                BidPrice = 8540 + (i * 0.5m),
                AskPrice = 8545 + (i * 0.5m),
                LastPrice = 8542.5m + (i * 0.5m),
                BidSize = 100,
                AskSize = 150,
                Currency = "USD"
            });
        }
    }

    public Task<DataFeed?> GetDataFeedAsync(string id)
        => Task.FromResult(_feeds.FirstOrDefault(f => f.Id == id));

    public Task<List<DataFeed>> GetAllDataFeedsAsync()
        => Task.FromResult(_feeds.ToList());

    public Task<DataFeed> CreateDataFeedAsync(DataFeed feed)
    {
        feed.Id = Guid.NewGuid().ToString();
        _feeds.Add(feed);
        return Task.FromResult(feed);
    }

    public Task<DataFeed> UpdateDataFeedAsync(DataFeed feed)
    {
        var existing = _feeds.FindIndex(f => f.Id == feed.Id);
        if (existing >= 0)
            _feeds[existing] = feed;
        return Task.FromResult(feed);
    }

    public Task ConnectAsync(string feedId)
    {
        var feed = _feeds.FirstOrDefault(f => f.Id == feedId);
        if (feed != null)
        {
            feed.ConnectionStatus = "Connected";
            feed.LastConnectedTime = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }

    public Task DisconnectAsync(string feedId)
    {
        var feed = _feeds.FirstOrDefault(f => f.Id == feedId);
        if (feed != null)
        {
            feed.ConnectionStatus = "Disconnected";
            feed.LastDisconnectedTime = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }

    public Task<FeedSubscription> SubscribeAsync(string feedId, FeedSubscription subscription)
    {
        var feed = _feeds.FirstOrDefault(f => f.Id == feedId);
        if (feed != null)
        {
            subscription.Id = Guid.NewGuid().ToString();
            subscription.SubscribedAt = DateTime.UtcNow;
            subscription.IsActive = true;
            feed.Subscriptions.Add(subscription);
        }
        return Task.FromResult(subscription);
    }

    public Task UnsubscribeAsync(string feedId, string subscriptionId)
    {
        var feed = _feeds.FirstOrDefault(f => f.Id == feedId);
        var sub = feed?.Subscriptions.FirstOrDefault(s => s.Id == subscriptionId);
        if (sub != null)
            sub.IsActive = false;
        return Task.CompletedTask;
    }

    public Task<List<PriceTick>> GetRecentTicksAsync(string source, MetalType metalType, int count = 100)
        => Task.FromResult(_ticks.Where(t => t.Source == source && t.MetalType == metalType).Take(count).ToList());

    public Task<DataFeed> GetFeedStatusAsync(string feedId)
        => Task.FromResult(_feeds.FirstOrDefault(f => f.Id == feedId) ?? throw new InvalidOperationException("Feed not found"));

    public Task<List<DataFeedError>> GetRecentErrorsAsync(string feedId)
    {
        var feed = _feeds.FirstOrDefault(f => f.Id == feedId);
        return Task.FromResult(feed?.RecentErrors ?? new List<DataFeedError>());
    }
}

/// <summary>
/// Mock implementation of membership tier service.
/// </summary>
public class MockMembershipTierService : IMembershipTierService
{
    private readonly List<MembershipTier> _tiers = new();
    private readonly Dictionary<string, string> _participantTiers = new();

    public MockMembershipTierService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _tiers.AddRange(new[]
        {
            new MembershipTier
            {
                Id = Guid.NewGuid().ToString(),
                TierCode = "GOLD",
                TierName = "Gold Member",
                TierLevel = 1,
                Description = "Full trading and clearing privileges with maximum position limits",
                MinimumCapital = 10000000m,
                MinimumCreditRating = "A-",
                CanTradeSpot = true,
                CanTradeFutures = true,
                CanTradeOptions = true,
                CanClearOwnTrades = true,
                CanClearForOthers = true,
                HasDirectMarketAccess = true,
                PositionLimitMultiplier = 2.0m,
                FeeStructure = new MembershipFeeStructure
                {
                    AnnualMembershipFee = 50000m,
                    TradingFeePerTon = 0.25m,
                    ClearingFeePerTrade = 5m,
                    VolumeDiscountThreshold = 10000m,
                    VolumeDiscountPercentage = 20m
                },
                IsActive = true
            },
            new MembershipTier
            {
                Id = Guid.NewGuid().ToString(),
                TierCode = "SILVER",
                TierName = "Silver Member",
                TierLevel = 2,
                Description = "Standard trading privileges with moderate position limits",
                MinimumCapital = 5000000m,
                MinimumCreditRating = "BBB",
                CanTradeSpot = true,
                CanTradeFutures = true,
                CanTradeOptions = false,
                CanClearOwnTrades = true,
                CanClearForOthers = false,
                HasDirectMarketAccess = true,
                PositionLimitMultiplier = 1.0m,
                FeeStructure = new MembershipFeeStructure
                {
                    AnnualMembershipFee = 25000m,
                    TradingFeePerTon = 0.35m,
                    ClearingFeePerTrade = 7.5m,
                    VolumeDiscountThreshold = 5000m,
                    VolumeDiscountPercentage = 10m
                },
                IsActive = true
            },
            new MembershipTier
            {
                Id = Guid.NewGuid().ToString(),
                TierCode = "BRONZE",
                TierName = "Bronze Member",
                TierLevel = 3,
                Description = "Basic spot trading privileges",
                MinimumCapital = 1000000m,
                CanTradeSpot = true,
                CanTradeFutures = false,
                CanTradeOptions = false,
                CanClearOwnTrades = false,
                HasDirectMarketAccess = false,
                PositionLimitMultiplier = 0.5m,
                FeeStructure = new MembershipFeeStructure
                {
                    AnnualMembershipFee = 10000m,
                    TradingFeePerTon = 0.50m,
                    ClearingFeePerTrade = 10m
                },
                IsActive = true
            }
        });

        _participantTiers["PART-001"] = "GOLD";
    }

    public Task<MembershipTier?> GetTierAsync(string tierCode)
        => Task.FromResult(_tiers.FirstOrDefault(t => t.TierCode == tierCode));

    public Task<List<MembershipTier>> GetAllTiersAsync()
        => Task.FromResult(_tiers.ToList());

    public Task<MembershipTier> CreateTierAsync(MembershipTier tier)
    {
        tier.Id = Guid.NewGuid().ToString();
        _tiers.Add(tier);
        return Task.FromResult(tier);
    }

    public Task<MembershipTier> UpdateTierAsync(MembershipTier tier)
    {
        var existing = _tiers.FindIndex(t => t.Id == tier.Id);
        if (existing >= 0)
            _tiers[existing] = tier;
        return Task.FromResult(tier);
    }

    public Task<bool> CheckEligibilityAsync(string participantId, string tierCode)
    {
        // Mock eligibility check - always return true for demo
        return Task.FromResult(true);
    }

    public Task<MembershipTier?> GetParticipantTierAsync(string participantId)
    {
        if (_participantTiers.TryGetValue(participantId, out var tierCode))
            return Task.FromResult(_tiers.FirstOrDefault(t => t.TierCode == tierCode));
        return Task.FromResult<MembershipTier?>(null);
    }

    public Task AssignTierAsync(string participantId, string tierCode)
    {
        _participantTiers[participantId] = tierCode;
        return Task.CompletedTask;
    }

    public Task<MembershipFeeStructure> GetFeeStructureAsync(string tierCode)
    {
        var tier = _tiers.FirstOrDefault(t => t.TierCode == tierCode);
        return Task.FromResult(tier?.FeeStructure ?? new MembershipFeeStructure());
    }

    public Task<decimal> CalculateTradingFeeAsync(string participantId, decimal tradeValue)
    {
        if (_participantTiers.TryGetValue(participantId, out var tierCode))
        {
            var tier = _tiers.FirstOrDefault(t => t.TierCode == tierCode);
            if (tier != null)
            {
                var baseFee = tradeValue * (tier.FeeStructure.TradingFeePercentage ?? 0.001m);
                return Task.FromResult(Math.Max(baseFee, tier.FeeStructure.MinimumTradingFee ?? 0));
            }
        }
        return Task.FromResult(tradeValue * 0.001m);
    }
}

/// <summary>
/// Mock implementation of onboarding workflow service.
/// </summary>
public class MockOnboardingService : IOnboardingService
{
    private readonly List<OnboardingWorkflow> _workflows = new();

    public MockOnboardingService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _workflows.Add(new OnboardingWorkflow
        {
            Id = Guid.NewGuid().ToString(),
            ApplicationNumber = "APP-2024-001",
            ApplicantId = "APPL-001",
            ApplicantName = "New Mining Company Ltd",
            ApplicantType = "Corporation",
            ApplyingForTier = "SILVER",
            CurrentStage = "DueDiligence",
            OverallStatus = "InProgress",
            CompletedStages = 2,
            TotalStages = 5,
            DocumentsSubmitted = 8,
            DocumentsVerified = 6,
            DocumentsPending = 2,
            AmlCheckCompleted = true,
            AmlCheckResult = "Passed",
            KycCheckCompleted = true,
            KycCheckResult = "Passed",
            CreditCheckCompleted = false,
            ApplicationFee = 500m,
            ApplicationFeePaid = true,
            AssignedReviewerId = "REV-001",
            AssignedReviewerName = "John Reviewer",
            ExpectedCompletionDate = DateTime.UtcNow.AddDays(7),
            Stages = new List<OnboardingStage>
            {
                new OnboardingStage { StageName = "Application", StageOrder = 1, Status = "Completed" },
                new OnboardingStage { StageName = "DocumentReview", StageOrder = 2, Status = "Completed" },
                new OnboardingStage { StageName = "DueDiligence", StageOrder = 3, Status = "InProgress" },
                new OnboardingStage { StageName = "Approval", StageOrder = 4, Status = "Pending" },
                new OnboardingStage { StageName = "Activation", StageOrder = 5, Status = "Pending" }
            },
            RequiredDocuments = new List<OnboardingDocument>
            {
                new OnboardingDocument { DocumentType = "CertificateOfIncorporation", DocumentName = "Certificate of Incorporation", Status = "Verified" },
                new OnboardingDocument { DocumentType = "TaxClearance", DocumentName = "ZRA Tax Clearance", Status = "Verified" },
                new OnboardingDocument { DocumentType = "BankReference", DocumentName = "Bank Reference Letter", Status = "UnderReview" },
                new OnboardingDocument { DocumentType = "FinancialStatements", DocumentName = "Audited Financial Statements", Status = "Pending" }
            }
        });
    }

    public Task<OnboardingWorkflow?> GetWorkflowAsync(string applicationNumber)
        => Task.FromResult(_workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber));

    public Task<List<OnboardingWorkflow>> GetAllWorkflowsAsync()
        => Task.FromResult(_workflows.ToList());

    public Task<List<OnboardingWorkflow>> GetPendingWorkflowsAsync()
        => Task.FromResult(_workflows.Where(w => w.OverallStatus == "InProgress").ToList());

    public Task<OnboardingWorkflow> CreateWorkflowAsync(OnboardingWorkflow workflow)
    {
        workflow.Id = Guid.NewGuid().ToString();
        workflow.ApplicationNumber = $"APP-{DateTime.UtcNow:yyyy}-{_workflows.Count + 1:D3}";
        workflow.ApplicationDate = DateTime.UtcNow;
        workflow.CurrentStage = "Application";
        workflow.OverallStatus = "InProgress";
        workflow.TotalStages = 5;
        _workflows.Add(workflow);
        return Task.FromResult(workflow);
    }

    public Task<OnboardingWorkflow> UpdateWorkflowAsync(OnboardingWorkflow workflow)
    {
        var existing = _workflows.FindIndex(w => w.Id == workflow.Id);
        if (existing >= 0)
            _workflows[existing] = workflow;
        return Task.FromResult(workflow);
    }

    public Task<OnboardingWorkflow> AdvanceStageAsync(string applicationNumber)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            var currentStage = workflow.Stages.FirstOrDefault(s => s.Status == "InProgress");
            if (currentStage != null)
            {
                currentStage.Status = "Completed";
                currentStage.CompletedDate = DateTime.UtcNow;
                workflow.CompletedStages++;
            }
            var nextStage = workflow.Stages.FirstOrDefault(s => s.Status == "Pending");
            if (nextStage != null)
            {
                nextStage.Status = "InProgress";
                nextStage.StartDate = DateTime.UtcNow;
                workflow.CurrentStage = nextStage.StageName;
            }
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task<OnboardingWorkflow> SubmitDocumentAsync(string applicationNumber, OnboardingDocument document)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            document.Id = Guid.NewGuid().ToString();
            document.SubmittedDate = DateTime.UtcNow;
            document.Status = "Submitted";
            workflow.RequiredDocuments.Add(document);
            workflow.DocumentsSubmitted++;
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task<OnboardingWorkflow> VerifyDocumentAsync(string applicationNumber, string documentId, bool approved, string? rejectionReason = null)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        var doc = workflow?.RequiredDocuments.FirstOrDefault(d => d.Id == documentId);
        if (doc != null)
        {
            doc.Status = approved ? "Verified" : "Rejected";
            doc.VerifiedDate = DateTime.UtcNow;
            if (!approved)
                doc.RejectionReason = rejectionReason;
            if (approved)
                workflow!.DocumentsVerified++;
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task<OnboardingWorkflow> CompleteAmlCheckAsync(string applicationNumber, string result)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            workflow.AmlCheckCompleted = true;
            workflow.AmlCheckResult = result;
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task<OnboardingWorkflow> CompleteKycCheckAsync(string applicationNumber, string result)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            workflow.KycCheckCompleted = true;
            workflow.KycCheckResult = result;
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task<OnboardingWorkflow> ApproveMembershipAsync(string applicationNumber, string approvedBy)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            workflow.OverallStatus = "Approved";
            workflow.ApprovedBy = approvedBy;
            workflow.ApprovalDate = DateTime.UtcNow;
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task<OnboardingWorkflow> RejectMembershipAsync(string applicationNumber, string reason)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            workflow.OverallStatus = "Rejected";
            workflow.RejectionReason = reason;
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task<OnboardingWorkflow> ActivateMembershipAsync(string applicationNumber)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            workflow.ActivationDate = DateTime.UtcNow;
            workflow.MembershipNumber = $"MBR-{DateTime.UtcNow:yyyy}-{_workflows.Count:D4}";
            workflow.ActualCompletionDate = DateTime.UtcNow;
            workflow.ProcessingDays = (DateTime.UtcNow - workflow.ApplicationDate).Days;
        }
        return Task.FromResult(workflow ?? throw new InvalidOperationException("Workflow not found"));
    }

    public Task AssignReviewerAsync(string applicationNumber, string reviewerId)
    {
        var workflow = _workflows.FirstOrDefault(w => w.ApplicationNumber == applicationNumber);
        if (workflow != null)
        {
            workflow.AssignedReviewerId = reviewerId;
        }
        return Task.CompletedTask;
    }
}

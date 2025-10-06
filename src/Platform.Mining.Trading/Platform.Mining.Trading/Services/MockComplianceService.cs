using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockComplianceService : IComplianceService
    {
        private List<ParticipantProfile> _profiles = new();
        private List<ComplianceDocument> _documents = new();
        private List<AccountLimit> _limits = new();

        public MockComplianceService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _profiles = new List<ParticipantProfile>
            {
                new ParticipantProfile
                {
                    ParticipantId = "PART-001",
                    EntityName = "Mining Ventures Ltd",
                    EntityType = "Corporation",
                    Country = "Zambia",
                    Email = "contact@miningventures.zm",
                    Phone = "+260-211-123456",
                    OnboardingStatus = "Approved",
                    SubmittedDate = DateTime.Now.AddDays(-30),
                    RiskScore = 25,
                    SanctionsCheckPassed = true,
                    PepCheckPassed = true
                },
                new ParticipantProfile
                {
                    ParticipantId = "PART-002",
                    EntityName = "Precious Metals Trading Inc",
                    EntityType = "Corporation",
                    Country = "South Africa",
                    Email = "info@preciousmetals.co.za",
                    Phone = "+27-11-987-6543",
                    OnboardingStatus = "Pending",
                    SubmittedDate = DateTime.Now.AddDays(-5),
                    RiskScore = 45,
                    SanctionsCheckPassed = true,
                    PepCheckPassed = false
                },
                new ParticipantProfile
                {
                    ParticipantId = "PART-003",
                    EntityName = "Global Resources Partners",
                    EntityType = "Partnership",
                    Country = "United Kingdom",
                    Email = "trading@globalresources.co.uk",
                    Phone = "+44-20-1234-5678",
                    OnboardingStatus = "Pending",
                    SubmittedDate = DateTime.Now.AddDays(-2),
                    RiskScore = 35,
                    SanctionsCheckPassed = true,
                    PepCheckPassed = true
                }
            };

            _documents = new List<ComplianceDocument>
            {
                new ComplianceDocument
                {
                    DocumentId = "DOC-001",
                    ParticipantId = "PART-001",
                    DocumentType = "Incorporation",
                    FileName = "certificate_of_incorporation.pdf",
                    UploadedDate = DateTime.Now.AddDays(-30),
                    Status = "Approved"
                },
                new ComplianceDocument
                {
                    DocumentId = "DOC-002",
                    ParticipantId = "PART-002",
                    DocumentType = "ID",
                    FileName = "director_id.pdf",
                    UploadedDate = DateTime.Now.AddDays(-5),
                    Status = "Pending"
                }
            };

            _limits = new List<AccountLimit>
            {
                new AccountLimit
                {
                    LimitId = "LMT-001",
                    ParticipantId = "PART-001",
                    LimitType = "TradingLimit",
                    LimitValue = 10000000,
                    Currency = "ZMW",
                    EffectiveDate = DateTime.Now.AddDays(-30)
                }
            };
        }

        public async Task<List<ParticipantProfile>> GetParticipantProfilesAsync()
        {
            await Task.Delay(100);
            return _profiles;
        }

        public async Task<ParticipantProfile> GetParticipantByIdAsync(string participantId)
        {
            await Task.Delay(50);
            return _profiles.FirstOrDefault(p => p.ParticipantId == participantId) ?? new ParticipantProfile();
        }

        public async Task<bool> ApproveParticipantAsync(string participantId)
        {
            await Task.Delay(150);
            var participant = _profiles.FirstOrDefault(p => p.ParticipantId == participantId);
            if (participant != null)
            {
                participant.OnboardingStatus = "Approved";
                return true;
            }
            return false;
        }

        public async Task<bool> RejectParticipantAsync(string participantId, string reason)
        {
            await Task.Delay(150);
            var participant = _profiles.FirstOrDefault(p => p.ParticipantId == participantId);
            if (participant != null)
            {
                participant.OnboardingStatus = "Rejected";
                return true;
            }
            return false;
        }

        public async Task<bool> RequestMoreDocumentsAsync(string participantId, string documentType)
        {
            await Task.Delay(100);
            // Simulate document request
            return true;
        }

        public async Task<List<ComplianceDocument>> GetDocumentsAsync(string participantId)
        {
            await Task.Delay(100);
            return _documents.Where(d => d.ParticipantId == participantId).ToList();
        }

        public async Task<bool> RunSanctionsCheckAsync(string participantId)
        {
            await Task.Delay(200);
            var participant = _profiles.FirstOrDefault(p => p.ParticipantId == participantId);
            if (participant != null)
            {
                participant.SanctionsCheckPassed = true;
                return true;
            }
            return false;
        }

        public async Task<bool> RunPepCheckAsync(string participantId)
        {
            await Task.Delay(200);
            var participant = _profiles.FirstOrDefault(p => p.ParticipantId == participantId);
            if (participant != null)
            {
                participant.PepCheckPassed = true;
                return true;
            }
            return false;
        }

        public async Task<List<AccountLimit>> GetAccountLimitsAsync(string participantId)
        {
            await Task.Delay(100);
            return _limits.Where(l => l.ParticipantId == participantId).ToList();
        }

        public async Task<bool> SetAccountLimitAsync(AccountLimit limit)
        {
            await Task.Delay(150);
            limit.LimitId = $"LMT-{_limits.Count + 1:D3}";
            limit.EffectiveDate = DateTime.Now;
            _limits.Add(limit);
            return true;
        }
    }
}

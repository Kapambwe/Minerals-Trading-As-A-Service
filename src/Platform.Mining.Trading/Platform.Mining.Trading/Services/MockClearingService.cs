using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockClearingService : IClearingService
    {
        private List<ClearingMember> _members = new();
        private List<NettingResult> _nettingResults = new();
        private List<SettlementObligation> _obligations = new();
        private List<MarginCall> _marginCalls = new();

        public MockClearingService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _members = new List<ClearingMember>
            {
                new ClearingMember
                {
                    MemberId = "CLR-MBR-001",
                    Name = "First Quantum Minerals",
                    Status = "Active",
                    MarginPosted = 5000000,
                    MarginRequired = 3500000,
                    ExcessMargin = 1500000
                },
                new ClearingMember
                {
                    MemberId = "CLR-MBR-002",
                    Name = "Mopani Copper Mines",
                    Status = "Active",
                    MarginPosted = 3000000,
                    MarginRequired = 2800000,
                    ExcessMargin = 200000
                },
                new ClearingMember
                {
                    MemberId = "CLR-MBR-003",
                    Name = "Gemfields Zambia",
                    Status = "Active",
                    MarginPosted = 2500000,
                    MarginRequired = 2700000,
                    ExcessMargin = -200000
                }
            };

            _nettingResults = new List<NettingResult>
            {
                new NettingResult
                {
                    NettingId = "NET-001",
                    NettingDate = DateTime.Today,
                    Currency = "ZMW",
                    GrossPayments = 15000000,
                    GrossReceipts = 18000000,
                    NetPosition = 3000000,
                    Status = "Completed"
                },
                new NettingResult
                {
                    NettingId = "NET-002",
                    NettingDate = DateTime.Today,
                    Currency = "USD",
                    GrossPayments = 800000,
                    GrossReceipts = 650000,
                    NetPosition = -150000,
                    Status = "Completed"
                }
            };

            _obligations = new List<SettlementObligation>
            {
                new SettlementObligation
                {
                    ObligationId = "OBL-001",
                    Member = "CLR-MBR-001",
                    SettlementDate = DateTime.Today.AddDays(2),
                    Currency = "ZMW",
                    Amount = 1500000,
                    Direction = "Pay",
                    Status = "Pending"
                },
                new SettlementObligation
                {
                    ObligationId = "OBL-002",
                    Member = "CLR-MBR-002",
                    SettlementDate = DateTime.Today.AddDays(2),
                    Currency = "USD",
                    Amount = 75000,
                    Direction = "Receive",
                    Status = "Pending"
                }
            };

            _marginCalls = new List<MarginCall>
            {
                new MarginCall
                {
                    CallId = "MC-001",
                    Member = "CLR-MBR-003",
                    IssueTime = DateTime.Now.AddHours(-2),
                    Amount = 200000,
                    DueTime = DateTime.Now.AddHours(2),
                    Status = "Issued"
                }
            };
        }

        public async Task<List<ClearingMember>> GetClearingMembersAsync()
        {
            await Task.Delay(100);
            return _members;
        }

        public async Task<List<NettingResult>> GetNettingResultsAsync(DateTime? date = null)
        {
            await Task.Delay(100);
            if (date.HasValue)
                return _nettingResults.Where(n => n.NettingDate.Date == date.Value.Date).ToList();
            return _nettingResults;
        }

        public async Task<List<SettlementObligation>> GetSettlementObligationsAsync()
        {
            await Task.Delay(100);
            return _obligations;
        }

        public async Task<bool> InitiateSettlementAsync(string obligationId)
        {
            await Task.Delay(150);
            var obligation = _obligations.FirstOrDefault(o => o.ObligationId == obligationId);
            if (obligation != null)
            {
                obligation.Status = "Settled";
                return true;
            }
            return false;
        }

        public async Task<bool> PerformNovationAsync(string tradeId)
        {
            await Task.Delay(150);
            // Simulate novation process
            return true;
        }

        public async Task<List<MarginCall>> GetMarginCallsAsync()
        {
            await Task.Delay(100);
            return _marginCalls;
        }

        public async Task<string> IssueMarginCallAsync(MarginCall call)
        {
            await Task.Delay(150);
            call.CallId = $"MC-{_marginCalls.Count + 1:D3}";
            call.IssueTime = DateTime.Now;
            call.Status = "Issued";
            _marginCalls.Add(call);
            return call.CallId;
        }

        public async Task<bool> CloseOutDefaultAsync(string memberId)
        {
            await Task.Delay(200);
            var member = _members.FirstOrDefault(m => m.MemberId == memberId);
            if (member != null)
            {
                member.Status = "Default";
                return true;
            }
            return false;
        }
    }
}

using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockMarketOperationsService : IMarketOperationsService
    {
        private MarketStatus _marketStatus = new MarketStatus
        {
            MarketId = "ZMX-001",
            Status = "Open",
            LastUpdated = DateTime.Now.AddHours(-2),
            UpdatedBy = "System",
            Reason = "Regular session opening"
        };

        private List<ScheduledEvent> _scheduledEvents = new();
        private List<CircuitBreaker> _circuitBreakers = new();
        private List<OverrideLog> _overrideLogs = new();

        public MockMarketOperationsService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _scheduledEvents = new List<ScheduledEvent>
            {
                new ScheduledEvent
                {
                    EventId = "EVT-001",
                    EventType = "Auction",
                    ScheduledTime = DateTime.Now.AddHours(1),
                    Instrument = "COPPER-JAN24",
                    Status = "Pending"
                },
                new ScheduledEvent
                {
                    EventId = "EVT-002",
                    EventType = "SessionClose",
                    ScheduledTime = DateTime.Now.AddHours(6),
                    Instrument = "All",
                    Status = "Pending"
                }
            };

            _circuitBreakers = new List<CircuitBreaker>
            {
                new CircuitBreaker
                {
                    BreakerId = "CB-001",
                    Instrument = "COPPER-JAN24",
                    PriceThreshold = 10000,
                    PercentageThreshold = 10,
                    IsActive = true
                },
                new CircuitBreaker
                {
                    BreakerId = "CB-002",
                    Instrument = "EMERALD-FEB24",
                    PriceThreshold = 150000,
                    PercentageThreshold = 15,
                    IsActive = true
                }
            };

            _overrideLogs = new List<OverrideLog>
            {
                new OverrideLog
                {
                    LogId = "OVR-001",
                    Timestamp = DateTime.Now.AddHours(-3),
                    User = "admin@zmx.com",
                    Action = "Emergency Halt Override",
                    Details = "Halted COPPER-JAN24 due to technical issue",
                    Justification = "Matching engine latency spike detected"
                }
            };
        }

        public async Task<MarketStatus> GetMarketStatusAsync()
        {
            await Task.Delay(50);
            return _marketStatus;
        }

        public async Task<bool> StartSessionAsync(string marketId)
        {
            await Task.Delay(100);
            _marketStatus.Status = "Open";
            _marketStatus.LastUpdated = DateTime.Now;
            _marketStatus.UpdatedBy = "MarketOps";
            _marketStatus.Reason = "Session started by operator";
            return true;
        }

        public async Task<bool> StopSessionAsync(string marketId, string reason)
        {
            await Task.Delay(100);
            _marketStatus.Status = "Closed";
            _marketStatus.LastUpdated = DateTime.Now;
            _marketStatus.UpdatedBy = "MarketOps";
            _marketStatus.Reason = reason;
            return true;
        }

        public async Task<bool> EmergencyHaltAsync(string marketId, string reason)
        {
            await Task.Delay(100);
            _marketStatus.Status = "Halted";
            _marketStatus.LastUpdated = DateTime.Now;
            _marketStatus.UpdatedBy = "MarketOps";
            _marketStatus.Reason = reason;
            
            _overrideLogs.Add(new OverrideLog
            {
                LogId = $"OVR-{_overrideLogs.Count + 1:D3}",
                Timestamp = DateTime.Now,
                User = "MarketOps",
                Action = "Emergency Halt",
                Details = $"Market {marketId} halted",
                Justification = reason
            });
            
            return true;
        }

        public async Task<List<ScheduledEvent>> GetScheduledEventsAsync()
        {
            await Task.Delay(50);
            return _scheduledEvents;
        }

        public async Task<string> ScheduleAuctionAsync(ScheduledEvent auctionEvent)
        {
            await Task.Delay(100);
            auctionEvent.EventId = $"EVT-{_scheduledEvents.Count + 1:D3}";
            auctionEvent.Status = "Pending";
            _scheduledEvents.Add(auctionEvent);
            return auctionEvent.EventId;
        }

        public async Task<List<CircuitBreaker>> GetCircuitBreakersAsync()
        {
            await Task.Delay(50);
            return _circuitBreakers;
        }

        public async Task<bool> UpdateCircuitBreakerAsync(CircuitBreaker breaker)
        {
            await Task.Delay(100);
            var existing = _circuitBreakers.FirstOrDefault(cb => cb.BreakerId == breaker.BreakerId);
            if (existing != null)
            {
                existing.PriceThreshold = breaker.PriceThreshold;
                existing.PercentageThreshold = breaker.PercentageThreshold;
                existing.IsActive = breaker.IsActive;
                return true;
            }
            return false;
        }

        public async Task<List<OverrideLog>> GetOverrideLogsAsync()
        {
            await Task.Delay(50);
            return _overrideLogs.OrderByDescending(log => log.Timestamp).ToList();
        }
    }
}

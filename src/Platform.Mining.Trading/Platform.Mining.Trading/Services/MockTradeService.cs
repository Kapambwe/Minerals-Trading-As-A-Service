using Platform.Mining.Trading.Models;
using System.Text;

namespace Platform.Mining.Trading.Services
{
    public class MockTradeService : ITradeService
    {
        private List<Trade> _trades = new();

        public MockTradeService()
        {
            InitializeSampleTrades();
        }

        private void InitializeSampleTrades()
        {
            _trades = new List<Trade>
            {
                new Trade
                {
                    TradeId = "TRD-2024-0045",
                    OrderId = "ORD-2024-001",
                    Instrument = "COPPER-JAN24",
                    Direction = "Buy",
                    Quantity = 200,
                    Price = 8500.00m,
                    TotalValue = 1700000.00m,
                    Fees = 8500.00m,
                    NetValue = 1708500.00m,
                    TradeTime = DateTime.Now.AddHours(-2),
                    Counterparty = "Mopani Copper Mines",
                    ClearingReference = "CLR-2024-0045",
                    SettlementDate = DateTime.Now.AddDays(2),
                    Status = "Executed",
                    Venue = "ZMX"
                },
                new Trade
                {
                    TradeId = "TRD-2024-0044",
                    OrderId = "ORD-2024-003",
                    Instrument = "EMERALD-FEB24",
                    Direction = "Sell",
                    Quantity = 50,
                    Price = 124000.00m,
                    TotalValue = 6200000.00m,
                    Fees = 31000.00m,
                    NetValue = 6169000.00m,
                    TradeTime = DateTime.Now.AddHours(-5),
                    Counterparty = "Gemfields Zambia",
                    ClearingReference = "CLR-2024-0044",
                    SettlementDate = DateTime.Now.AddDays(2),
                    Status = "Settled",
                    Venue = "ZMX"
                },
                new Trade
                {
                    TradeId = "TRD-2024-0043",
                    OrderId = "ORD-2024-002",
                    Instrument = "COBALT-MAR24",
                    Direction = "Buy",
                    Quantity = 250,
                    Price = 34500.00m,
                    TotalValue = 8625000.00m,
                    Fees = 43125.00m,
                    NetValue = 8668125.00m,
                    TradeTime = DateTime.Now.AddDays(-1),
                    Counterparty = "KCM Trading",
                    ClearingReference = "CLR-2024-0043",
                    SettlementDate = DateTime.Now.AddDays(1),
                    Status = "Pending Settlement",
                    Venue = "ZMX"
                },
                new Trade
                {
                    TradeId = "TRD-2024-0042",
                    OrderId = "ORD-2024-004",
                    Instrument = "COPPER-JAN24",
                    Direction = "Sell",
                    Quantity = 300,
                    Price = 8450.00m,
                    TotalValue = 2535000.00m,
                    Fees = 12675.00m,
                    NetValue = 2522325.00m,
                    TradeTime = DateTime.Now.AddDays(-2),
                    Counterparty = "First Quantum Minerals",
                    ClearingReference = "CLR-2024-0042",
                    SettlementDate = DateTime.Now,
                    Status = "Settled",
                    Venue = "ZMX"
                }
            };
        }

        public async Task<List<Trade>> GetTradesAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            await Task.Delay(100);
            
            var filteredTrades = _trades.AsQueryable();
            
            if (fromDate.HasValue)
                filteredTrades = filteredTrades.Where(t => t.TradeTime >= fromDate.Value);
            
            if (toDate.HasValue)
                filteredTrades = filteredTrades.Where(t => t.TradeTime <= toDate.Value);

            return filteredTrades.OrderByDescending(t => t.TradeTime).ToList();
        }

        public async Task<Trade> GetTradeByIdAsync(string tradeId)
        {
            await Task.Delay(50);
            return _trades.FirstOrDefault(t => t.TradeId == tradeId) ?? new Trade();
        }

        public async Task<byte[]> ExportTradesAsync(List<Trade> trades, string format = "CSV")
        {
            await Task.Delay(200);
            
            if (format.ToUpper() == "CSV")
            {
                var sb = new StringBuilder();
                sb.AppendLine("TradeId,Instrument,Direction,Quantity,Price,TotalValue,Fees,NetValue,TradeTime,Counterparty,Status");
                
                foreach (var trade in trades)
                {
                    sb.AppendLine($"{trade.TradeId},{trade.Instrument},{trade.Direction},{trade.Quantity}," +
                                 $"{trade.Price},{trade.TotalValue},{trade.Fees},{trade.NetValue}," +
                                 $"{trade.TradeTime:yyyy-MM-dd HH:mm:ss},{trade.Counterparty},{trade.Status}");
                }
                
                return Encoding.UTF8.GetBytes(sb.ToString());
            }
            
            return Array.Empty<byte>();
        }

        public async Task<bool> RequestConfirmationAsync(string tradeId)
        {
            await Task.Delay(100);
            var trade = _trades.FirstOrDefault(t => t.TradeId == tradeId);
            return trade != null;
        }

        public async Task<bool> DisputeTradeAsync(string tradeId, string reason)
        {
            await Task.Delay(150);
            var trade = _trades.FirstOrDefault(t => t.TradeId == tradeId);
            if (trade != null)
            {
                trade.Status = "Disputed";
                return true;
            }
            return false;
        }
    }
}

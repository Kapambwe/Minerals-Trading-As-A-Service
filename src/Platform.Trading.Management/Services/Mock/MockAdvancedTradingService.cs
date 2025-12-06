using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Trading;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of forex service.
/// </summary>
public class MockForexService : IForexService
{
    private readonly List<ForexRate> _rates = new();

    public MockForexService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _rates.AddRange(new[]
        {
            new ForexRate
            {
                Id = Guid.NewGuid().ToString(),
                BaseCurrency = "USD",
                QuoteCurrency = "ZMW",
                Rate = 26.85m,
                BidRate = 26.75m,
                AskRate = 26.95m,
                PreviousClose = 26.80m,
                Change = 0.05m,
                ChangePercentage = 0.19m,
                RateSource = "BOZ",
                RateType = "Official",
                ValidFrom = DateTime.UtcNow.Date,
                ValidUntil = DateTime.UtcNow.Date.AddDays(1),
                IsActive = true
            },
            new ForexRate
            {
                Id = Guid.NewGuid().ToString(),
                BaseCurrency = "USD",
                QuoteCurrency = "EUR",
                Rate = 0.92m,
                BidRate = 0.918m,
                AskRate = 0.922m,
                RateSource = "Reuters",
                RateType = "Mid",
                ValidFrom = DateTime.UtcNow.Date,
                ValidUntil = DateTime.UtcNow.Date.AddDays(1),
                IsActive = true
            },
            new ForexRate
            {
                Id = Guid.NewGuid().ToString(),
                BaseCurrency = "USD",
                QuoteCurrency = "CNY",
                Rate = 7.24m,
                BidRate = 7.235m,
                AskRate = 7.245m,
                RateSource = "Bloomberg",
                RateType = "Mid",
                ValidFrom = DateTime.UtcNow.Date,
                ValidUntil = DateTime.UtcNow.Date.AddDays(1),
                IsActive = true
            }
        });
    }

    public Task<ForexRate?> GetRateAsync(string baseCurrency, string quoteCurrency)
        => Task.FromResult(_rates.FirstOrDefault(r => r.BaseCurrency == baseCurrency && r.QuoteCurrency == quoteCurrency && r.IsActive));

    public Task<List<ForexRate>> GetAllRatesAsync()
        => Task.FromResult(_rates.Where(r => r.IsActive).ToList());

    public Task<ForexRate> UpdateRateAsync(ForexRate rate)
    {
        var existing = _rates.FindIndex(r => r.Id == rate.Id);
        if (existing >= 0)
            _rates[existing] = rate;
        rate.LastUpdated = DateTime.UtcNow;
        return Task.FromResult(rate);
    }

    public Task<ForexConversion> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency)
    {
        var rate = _rates.FirstOrDefault(r => r.BaseCurrency == fromCurrency && r.QuoteCurrency == toCurrency);
        var convertedAmount = rate != null ? amount * rate.Rate : amount;
        
        return Task.FromResult(new ForexConversion
        {
            Id = Guid.NewGuid().ToString(),
            FromCurrency = fromCurrency,
            FromAmount = amount,
            ToCurrency = toCurrency,
            ToAmount = convertedAmount,
            ExchangeRate = rate?.Rate ?? 1m,
            RateType = "Spot",
            RateSource = rate?.RateSource,
            RateDate = DateTime.UtcNow,
            Status = "Completed"
        });
    }

    public Task<List<ForexRate>> GetRateHistoryAsync(string baseCurrency, string quoteCurrency, DateTime fromDate, DateTime toDate)
        => Task.FromResult(_rates.Where(r => r.BaseCurrency == baseCurrency && r.QuoteCurrency == quoteCurrency).ToList());

    public Task RefreshRatesFromBozAsync()
    {
        foreach (var rate in _rates.Where(r => r.RateSource == "BOZ"))
        {
            rate.LastUpdated = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }
}

/// <summary>
/// Mock implementation of block trade service.
/// </summary>
public class MockBlockTradeService : IBlockTradeService
{
    private readonly List<BlockTrade> _blockTrades = new();

    public MockBlockTradeService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _blockTrades.Add(new BlockTrade
        {
            Id = Guid.NewGuid().ToString(),
            BlockTradeNumber = "BT-2024-001",
            MetalType = MetalType.Copper,
            QualityGrade = "LME Grade A",
            Quantity = 500,
            MinimumBlockSize = 100,
            NegotiatedPrice = 8550m,
            Currency = "USD",
            ReferencePrice = 8545m,
            PriceDeviation = 0.06m,
            BuyerId = "BUYER-001",
            BuyerName = "Glencore International",
            SellerId = "SELLER-001",
            SellerName = "Konkola Copper Mines",
            NegotiationType = "Negotiated",
            Status = "Approved",
            ApprovalDate = DateTime.UtcNow.AddDays(-1),
            ReportedToMarket = true,
            MarketReportTime = DateTime.UtcNow.AddDays(-1).AddMinutes(15)
        });
    }

    public Task<BlockTrade?> GetBlockTradeAsync(string id)
        => Task.FromResult(_blockTrades.FirstOrDefault(t => t.Id == id));

    public Task<List<BlockTrade>> GetAllBlockTradesAsync()
        => Task.FromResult(_blockTrades.ToList());

    public Task<BlockTrade> CreateBlockTradeAsync(BlockTrade trade)
    {
        trade.Id = Guid.NewGuid().ToString();
        trade.BlockTradeNumber = $"BT-{DateTime.UtcNow:yyyy}-{_blockTrades.Count + 1:D3}";
        trade.TradeDate = DateTime.UtcNow;
        trade.Status = "Pending";
        _blockTrades.Add(trade);
        return Task.FromResult(trade);
    }

    public Task<BlockTrade> ApproveBlockTradeAsync(string id, string approvedBy)
    {
        var trade = _blockTrades.FirstOrDefault(t => t.Id == id);
        if (trade != null)
        {
            trade.Status = "Approved";
            trade.ApprovalDate = DateTime.UtcNow;
            trade.ApprovedBy = approvedBy;
        }
        return Task.FromResult(trade ?? throw new InvalidOperationException("Trade not found"));
    }

    public Task<BlockTrade> RejectBlockTradeAsync(string id, string reason)
    {
        var trade = _blockTrades.FirstOrDefault(t => t.Id == id);
        if (trade != null)
        {
            trade.Status = "Rejected";
            trade.RejectionReason = reason;
        }
        return Task.FromResult(trade ?? throw new InvalidOperationException("Trade not found"));
    }

    public Task<bool> ValidatePriceDeviationAsync(BlockTrade trade)
    {
        if (trade.ReferencePrice.HasValue && trade.MaxAllowedDeviation.HasValue)
        {
            var deviation = Math.Abs((trade.NegotiatedPrice - trade.ReferencePrice.Value) / trade.ReferencePrice.Value) * 100;
            return Task.FromResult(deviation <= trade.MaxAllowedDeviation.Value);
        }
        return Task.FromResult(true);
    }

    public Task ReportToMarketAsync(string id)
    {
        var trade = _blockTrades.FirstOrDefault(t => t.Id == id);
        if (trade != null)
        {
            trade.ReportedToMarket = true;
            trade.MarketReportTime = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }

    public Task<List<BlockTrade>> GetPendingApprovalsAsync()
        => Task.FromResult(_blockTrades.Where(t => t.Status == "Pending").ToList());
}

/// <summary>
/// Mock implementation of derivative contract service.
/// </summary>
public class MockDerivativeService : IDerivativeService
{
    private readonly List<DerivativeContract> _contracts = new();

    public MockDerivativeService()
    {
        InitializeMockData();
    }

    private void InitializeMockData()
    {
        _contracts.AddRange(new[]
        {
            new DerivativeContract
            {
                Id = Guid.NewGuid().ToString(),
                ContractCode = "CU-MAR24",
                ContractName = "Copper Futures March 2024",
                ContractType = "Futures",
                MetalType = MetalType.Copper,
                QualityGrade = "LME Grade A",
                ContractSize = 25,
                TickSize = 0.50m,
                TickValue = 12.50m,
                Currency = "USD",
                ContractMonth = "Mar24",
                ExpiryDate = new DateTime(2024, 3, 15),
                FirstNoticeDate = new DateTime(2024, 3, 1),
                LastTradingDate = new DateTime(2024, 3, 14),
                SettlementType = "Physical",
                SettlementPrice = 8545m,
                LastPrice = 8550m,
                Volume = 15000,
                OpenInterest = 45000,
                InitialMargin = 5000m,
                MaintenanceMargin = 4000m,
                TradingStatus = "Active"
            },
            new DerivativeContract
            {
                Id = Guid.NewGuid().ToString(),
                ContractCode = "CO-JUN24",
                ContractName = "Cobalt Futures June 2024",
                ContractType = "Futures",
                MetalType = MetalType.Cobalt,
                ContractSize = 1,
                TickSize = 1.00m,
                TickValue = 1.00m,
                Currency = "USD",
                ContractMonth = "Jun24",
                ExpiryDate = new DateTime(2024, 6, 21),
                SettlementType = "Cash",
                SettlementPrice = 35000m,
                LastPrice = 35250m,
                InitialMargin = 7000m,
                MaintenanceMargin = 5600m,
                TradingStatus = "Active"
            }
        });
    }

    public Task<DerivativeContract?> GetContractAsync(string contractCode)
        => Task.FromResult(_contracts.FirstOrDefault(c => c.ContractCode == contractCode));

    public Task<List<DerivativeContract>> GetAllContractsAsync()
        => Task.FromResult(_contracts.ToList());

    public Task<List<DerivativeContract>> GetActiveContractsAsync(MetalType? metalType = null)
    {
        var query = _contracts.Where(c => c.TradingStatus == "Active");
        if (metalType.HasValue)
            query = query.Where(c => c.MetalType == metalType.Value);
        return Task.FromResult(query.ToList());
    }

    public Task<DerivativeContract> CreateContractAsync(DerivativeContract contract)
    {
        contract.Id = Guid.NewGuid().ToString();
        _contracts.Add(contract);
        return Task.FromResult(contract);
    }

    public Task<DerivativeContract> UpdateContractAsync(DerivativeContract contract)
    {
        var existing = _contracts.FindIndex(c => c.Id == contract.Id);
        if (existing >= 0)
            _contracts[existing] = contract;
        return Task.FromResult(contract);
    }

    public Task<List<DerivativeContract>> GetExpiringContractsAsync(int daysToExpiry)
    {
        var expiryDate = DateTime.UtcNow.AddDays(daysToExpiry);
        return Task.FromResult(_contracts.Where(c => c.ExpiryDate <= expiryDate && c.TradingStatus == "Active").ToList());
    }

    public Task<OptionContract?> GetOptionContractAsync(string contractCode)
        => Task.FromResult<OptionContract?>(null);

    public Task<OptionContract> CreateOptionContractAsync(OptionContract contract)
    {
        contract.Id = Guid.NewGuid().ToString();
        return Task.FromResult(contract);
    }

    public Task SettleContractAsync(string contractCode)
    {
        var contract = _contracts.FirstOrDefault(c => c.ContractCode == contractCode);
        if (contract != null)
            contract.TradingStatus = "Settled";
        return Task.CompletedTask;
    }

    public Task UpdateSettlementPriceAsync(string contractCode, decimal price)
    {
        var contract = _contracts.FirstOrDefault(c => c.ContractCode == contractCode);
        if (contract != null)
            contract.SettlementPrice = price;
        return Task.CompletedTask;
    }
}

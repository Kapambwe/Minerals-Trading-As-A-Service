using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Trading;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Interface for forex rate management service.
/// Addresses Gap TO-004: Multi-Currency Support with real-time FX integration.
/// </summary>
public interface IForexService
{
    Task<ForexRate?> GetRateAsync(string baseCurrency, string quoteCurrency);
    Task<List<ForexRate>> GetAllRatesAsync();
    Task<ForexRate> UpdateRateAsync(ForexRate rate);
    Task<ForexConversion> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency);
    Task<List<ForexRate>> GetRateHistoryAsync(string baseCurrency, string quoteCurrency, DateTime fromDate, DateTime toDate);
    Task RefreshRatesFromBozAsync();
}

/// <summary>
/// Interface for block trade management service.
/// Addresses Gap TO-007: Block Trading with price protection.
/// </summary>
public interface IBlockTradeService
{
    Task<BlockTrade?> GetBlockTradeAsync(string id);
    Task<List<BlockTrade>> GetAllBlockTradesAsync();
    Task<BlockTrade> CreateBlockTradeAsync(BlockTrade trade);
    Task<BlockTrade> ApproveBlockTradeAsync(string id, string approvedBy);
    Task<BlockTrade> RejectBlockTradeAsync(string id, string reason);
    Task<bool> ValidatePriceDeviationAsync(BlockTrade trade);
    Task ReportToMarketAsync(string id);
    Task<List<BlockTrade>> GetPendingApprovalsAsync();
}

/// <summary>
/// Interface for derivative contract management service.
/// Addresses Gap TO-005: Futures & Options Contracts support.
/// </summary>
public interface IDerivativeService
{
    Task<DerivativeContract?> GetContractAsync(string contractCode);
    Task<List<DerivativeContract>> GetAllContractsAsync();
    Task<List<DerivativeContract>> GetActiveContractsAsync(MetalType? metalType = null);
    Task<DerivativeContract> CreateContractAsync(DerivativeContract contract);
    Task<DerivativeContract> UpdateContractAsync(DerivativeContract contract);
    Task<List<DerivativeContract>> GetExpiringContractsAsync(int daysToExpiry);
    Task<OptionContract?> GetOptionContractAsync(string contractCode);
    Task<OptionContract> CreateOptionContractAsync(OptionContract contract);
    Task SettleContractAsync(string contractCode);
    Task UpdateSettlementPriceAsync(string contractCode, decimal price);
}

using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IMatchingEngineService
    {
        Task<MatchingEngineConfig> GetEngineConfigAsync();
        Task<List<MatchQueue>> GetMatchQueuesAsync();
        Task<List<UnmatchedOrder>> GetUnmatchedOrdersAsync();
        Task<List<EngineParameter>> GetEngineParametersAsync();
        Task<bool> ApplyParameterUpdateAsync(string parameterId, string newValue);
        Task<bool> RestartEngineAsync();
        Task<bool> EmergencyStopAsync(string reason);
        Task<bool> ReplayTradesAsync(DateTime fromDate, DateTime toDate);
        Task<List<ContractSpec>> GetContractSpecsAsync();
    }

    public interface IProductDefinitionService
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<string> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeprecateProductAsync(string productId);
        Task<List<ProductVersion>> GetProductVersionsAsync(string productId);
        Task<string> CreateProductVersionAsync(ProductVersion version);
        Task<List<InstrumentImpact>> PreviewProductImpactAsync(string productId);
    }

    public interface ISettlementEngineService
    {
        Task<List<BankEndpoint>> GetBankEndpointsAsync();
        Task<bool> ConfigureBankEndpointAsync(BankEndpoint endpoint);
        Task<List<SettlementBatch>> GetSettlementBatchesAsync();
        Task<string> TriggerSettlementAsync(SettlementBatch batch);
        Task<bool> RollbackSettlementAsync(string batchId);
        Task<bool> PushSwiftFileAsync(string batchId);
        Task<List<PaymentTransaction>> GetTransactionsAsync(string batchId);
        Task<List<FxConversionRule>> GetFxRulesAsync();
        Task<bool> UpdateFxRuleAsync(FxConversionRule rule);
    }

    public interface ISimulationService
    {
        Task<List<SimulationScenario>> GetScenariosAsync();
        Task<string> CreateScenarioAsync(SimulationScenario scenario);
        Task<bool> RunSimulationAsync(string scenarioId);
        Task<SimulationResult> GetSimulationResultAsync(string scenarioId);
        Task<List<MarketDataSnapshot>> GetMarketDataSnapshotsAsync();
        Task<byte[]> ExportResultsAsync(string scenarioId);
        Task<List<ParticipantSimulation>> GetSimulatedParticipantsAsync(string scenarioId);
        Task<List<BacktestMetric>> GetBacktestMetricsAsync(string scenarioId);
    }
}

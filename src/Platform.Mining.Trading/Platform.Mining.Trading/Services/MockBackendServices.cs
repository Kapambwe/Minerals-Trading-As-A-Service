using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockMatchingEngineService : IMatchingEngineService
    {
        private MatchingEngineConfig _engineConfig = new();
        private List<MatchQueue> _queues = new();
        private List<UnmatchedOrder> _unmatchedOrders = new();
        private List<EngineParameter> _parameters = new();
        private List<ContractSpec> _contractSpecs = new();

        public MockMatchingEngineService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _engineConfig = new MatchingEngineConfig
            {
                ConfigId = "ENG-001",
                MatchingRule = "Price-Time Priority",
                TickSize = 0.01m,
                Status = "Running",
                LastRestart = DateTime.Now.AddHours(-12),
                CurrentQueueDepth = 145,
                ProcessedOrdersPerSecond = 2500
            };

            _queues = new List<MatchQueue>
            {
                new MatchQueue
                {
                    QueueId = "Q-COPPER-JAN24",
                    Instrument = "COPPER-JAN24",
                    BuyOrderCount = 45,
                    SellOrderCount = 38,
                    UnmatchedOrderCount = 12,
                    LastProcessedTime = DateTime.Now.AddSeconds(-2),
                    AverageMatchLatency = 3.5m
                },
                new MatchQueue
                {
                    QueueId = "Q-EMERALD-FEB24",
                    Instrument = "EMERALD-FEB24",
                    BuyOrderCount = 28,
                    SellOrderCount = 31,
                    UnmatchedOrderCount = 5,
                    LastProcessedTime = DateTime.Now.AddSeconds(-1),
                    AverageMatchLatency = 2.8m
                }
            };

            _unmatchedOrders = new List<UnmatchedOrder>
            {
                new UnmatchedOrder
                {
                    OrderId = "ORD-UNMATCH-001",
                    Instrument = "COPPER-JAN24",
                    Direction = "Buy",
                    Quantity = 100,
                    Price = 8450.00m,
                    Reason = "Price outside acceptable range",
                    SubmittedTime = DateTime.Now.AddMinutes(-15)
                },
                new UnmatchedOrder
                {
                    OrderId = "ORD-UNMATCH-002",
                    Instrument = "COBALT-MAR24",
                    Direction = "Sell",
                    Quantity = 50,
                    Price = 35500.00m,
                    Reason = "No matching counterparty",
                    SubmittedTime = DateTime.Now.AddMinutes(-10)
                }
            };

            _parameters = new List<EngineParameter>
            {
                new EngineParameter
                {
                    ParameterId = "PARAM-001",
                    ParameterName = "MaxOrdersPerSecond",
                    CurrentValue = "5000",
                    ProposedValue = "7500",
                    ApprovalStatus = "Pending",
                    ProposedBy = "sre@zmx.com"
                },
                new EngineParameter
                {
                    ParameterId = "PARAM-002",
                    ParameterName = "MatchingLatencyThreshold",
                    CurrentValue = "10",
                    ProposedValue = "10",
                    ApprovalStatus = "Approved",
                    ProposedBy = "marketops@zmx.com",
                    ApprovedDate = DateTime.Now.AddDays(-5)
                }
            };

            _contractSpecs = new List<ContractSpec>
            {
                new ContractSpec
                {
                    ContractId = "SPEC-COPPER",
                    Instrument = "COPPER-*",
                    TickSize = 10.00m,
                    MinQuantity = 10,
                    MaxQuantity = 10000,
                    ContractSize = 1000,
                    PricingUnit = "ZMW per tonne"
                }
            };
        }

        public async Task<MatchingEngineConfig> GetEngineConfigAsync()
        {
            await Task.Delay(50);
            return _engineConfig;
        }

        public async Task<List<MatchQueue>> GetMatchQueuesAsync()
        {
            await Task.Delay(100);
            return _queues;
        }

        public async Task<List<UnmatchedOrder>> GetUnmatchedOrdersAsync()
        {
            await Task.Delay(100);
            return _unmatchedOrders;
        }

        public async Task<List<EngineParameter>> GetEngineParametersAsync()
        {
            await Task.Delay(100);
            return _parameters;
        }

        public async Task<bool> ApplyParameterUpdateAsync(string parameterId, string newValue)
        {
            await Task.Delay(150);
            var param = _parameters.FirstOrDefault(p => p.ParameterId == parameterId);
            if (param != null && param.ApprovalStatus == "Approved")
            {
                param.CurrentValue = newValue;
                return true;
            }
            return false;
        }

        public async Task<bool> RestartEngineAsync()
        {
            await Task.Delay(500);
            _engineConfig.LastRestart = DateTime.Now;
            _engineConfig.Status = "Running";
            return true;
        }

        public async Task<bool> EmergencyStopAsync(string reason)
        {
            await Task.Delay(200);
            _engineConfig.Status = "Stopped";
            return true;
        }

        public async Task<bool> ReplayTradesAsync(DateTime fromDate, DateTime toDate)
        {
            await Task.Delay(1000);
            return true;
        }

        public async Task<List<ContractSpec>> GetContractSpecsAsync()
        {
            await Task.Delay(100);
            return _contractSpecs;
        }
    }

    public class MockProductDefinitionService : IProductDefinitionService
    {
        private List<Product> _products = new();
        private List<ProductVersion> _versions = new();

        public MockProductDefinitionService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _products = new List<Product>
            {
                new Product
                {
                    ProductId = "PROD-COPPER-001",
                    ProductName = "Copper Futures",
                    CommodityType = "Base Metal",
                    GradeSpec = "High Grade (99.9% purity)",
                    ContractSize = 1000,
                    PricingUnit = "ZMW per tonne",
                    TickSize = 10.00m,
                    MinLotSize = 10,
                    MaxLotSize = 10000,
                    DeliveryRules = "Physical delivery at approved warehouses",
                    DeliveryMonths = new List<string> { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" },
                    ExpiryDate = DateTime.Now.AddYears(1),
                    Status = "Active",
                    Version = 2,
                    CreatedDate = DateTime.Now.AddMonths(-6),
                    CreatedBy = "product@zmx.com"
                },
                new Product
                {
                    ProductId = "PROD-EMERALD-001",
                    ProductName = "Emerald Futures",
                    CommodityType = "Precious Stone",
                    GradeSpec = "Premium Grade (AA+)",
                    ContractSize = 100,
                    PricingUnit = "ZMW per carat",
                    TickSize = 100.00m,
                    MinLotSize = 1,
                    MaxLotSize = 1000,
                    DeliveryRules = "Cash settlement or physical at vault",
                    DeliveryMonths = new List<string> { "Jan", "Apr", "Jul", "Oct" },
                    ExpiryDate = DateTime.Now.AddMonths(18),
                    Status = "Active",
                    Version = 1,
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    CreatedBy = "product@zmx.com"
                }
            };

            _versions = new List<ProductVersion>
            {
                new ProductVersion
                {
                    VersionId = "VER-001",
                    ProductId = "PROD-COPPER-001",
                    VersionNumber = 2,
                    Changes = "Updated tick size from 5 to 10 ZMW",
                    EffectiveDate = DateTime.Now.AddMonths(-2),
                    ApprovedBy = "marketops@zmx.com"
                }
            };
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            await Task.Delay(100);
            return _products;
        }

        public async Task<Product> GetProductByIdAsync(string productId)
        {
            await Task.Delay(50);
            return _products.FirstOrDefault(p => p.ProductId == productId) ?? new Product();
        }

        public async Task<string> CreateProductAsync(Product product)
        {
            await Task.Delay(200);
            product.ProductId = $"PROD-{_products.Count + 1:D3}";
            product.CreatedDate = DateTime.Now;
            product.Version = 1;
            product.Status = "Draft";
            _products.Add(product);
            return product.ProductId;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            await Task.Delay(150);
            var existing = _products.FirstOrDefault(p => p.ProductId == product.ProductId);
            if (existing != null)
            {
                product.Version = existing.Version + 1;
                _products.Remove(existing);
                _products.Add(product);
                return true;
            }
            return false;
        }

        public async Task<bool> DeprecateProductAsync(string productId)
        {
            await Task.Delay(100);
            var product = _products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.Status = "Deprecated";
                return true;
            }
            return false;
        }

        public async Task<List<ProductVersion>> GetProductVersionsAsync(string productId)
        {
            await Task.Delay(100);
            return _versions.Where(v => v.ProductId == productId).ToList();
        }

        public async Task<string> CreateProductVersionAsync(ProductVersion version)
        {
            await Task.Delay(150);
            version.VersionId = $"VER-{_versions.Count + 1:D3}";
            _versions.Add(version);
            return version.VersionId;
        }

        public async Task<List<InstrumentImpact>> PreviewProductImpactAsync(string productId)
        {
            await Task.Delay(200);
            return new List<InstrumentImpact>
            {
                new InstrumentImpact
                {
                    InstrumentId = "COPPER-JAN24",
                    InstrumentName = "Copper January 2024",
                    OpenPositions = 150,
                    OpenOrders = 45,
                    ImpactLevel = "Medium",
                    RequiredAction = "Notify participants 24h before change"
                },
                new InstrumentImpact
                {
                    InstrumentId = "COPPER-FEB24",
                    InstrumentName = "Copper February 2024",
                    OpenPositions = 200,
                    OpenOrders = 60,
                    ImpactLevel = "High",
                    RequiredAction = "Close out positions or migrate to new specification"
                }
            };
        }
    }

    public class MockSettlementEngineService : ISettlementEngineService
    {
        private List<BankEndpoint> _endpoints = new();
        private List<SettlementBatch> _batches = new();
        private List<PaymentTransaction> _transactions = new();
        private List<FxConversionRule> _fxRules = new();

        public MockSettlementEngineService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _endpoints = new List<BankEndpoint>
            {
                new BankEndpoint
                {
                    EndpointId = "BANK-001",
                    BankName = "Zambia National Bank",
                    EndpointType = "RTGS",
                    ConnectionString = "rtgs://znb.zm:8443/settlement",
                    IsActive = true,
                    LastSuccessfulConnection = DateTime.Now.AddMinutes(-5),
                    TimeoutSeconds = 30,
                    MaxRetries = 3
                },
                new BankEndpoint
                {
                    EndpointId = "BANK-002",
                    BankName = "International SWIFT Gateway",
                    EndpointType = "SWIFT",
                    ConnectionString = "swift://gateway.swift.com/zmx",
                    IsActive = true,
                    LastSuccessfulConnection = DateTime.Now.AddMinutes(-10),
                    TimeoutSeconds = 60,
                    MaxRetries = 5
                }
            };

            _batches = new List<SettlementBatch>
            {
                new SettlementBatch
                {
                    BatchId = "BATCH-2024-001",
                    SettlementDate = DateTime.Today,
                    Currency = "ZMW",
                    TotalTransactions = 156,
                    TotalAmount = 45000000.00m,
                    Status = "Completed",
                    ProcessedTime = DateTime.Now.AddHours(-2),
                    ProcessedBy = "clearing@zmx.com"
                },
                new SettlementBatch
                {
                    BatchId = "BATCH-2024-002",
                    SettlementDate = DateTime.Today,
                    Currency = "USD",
                    TotalTransactions = 45,
                    TotalAmount = 2500000.00m,
                    Status = "Pending",
                    ProcessedBy = string.Empty
                }
            };

            _transactions = new List<PaymentTransaction>
            {
                new PaymentTransaction
                {
                    TransactionId = "TXN-001",
                    BatchId = "BATCH-2024-001",
                    FromAccount = "ACC-001",
                    ToAccount = "ACC-002",
                    Amount = 150000.00m,
                    Currency = "ZMW",
                    PaymentMethod = "RTGS",
                    Status = "Confirmed",
                    ReferenceNumber = "REF-2024-001",
                    CompletedTime = DateTime.Now.AddHours(-2)
                }
            };

            _fxRules = new List<FxConversionRule>
            {
                new FxConversionRule
                {
                    RuleId = "FX-001",
                    FromCurrency = "USD",
                    ToCurrency = "ZMW",
                    ExchangeRate = 18.50m,
                    EffectiveDate = DateTime.Today,
                    Source = "Bank of Zambia",
                    IsActive = true
                },
                new FxConversionRule
                {
                    RuleId = "FX-002",
                    FromCurrency = "EUR",
                    ToCurrency = "ZMW",
                    ExchangeRate = 20.25m,
                    EffectiveDate = DateTime.Today,
                    Source = "Bank of Zambia",
                    IsActive = true
                }
            };
        }

        public async Task<List<BankEndpoint>> GetBankEndpointsAsync()
        {
            await Task.Delay(100);
            return _endpoints;
        }

        public async Task<bool> ConfigureBankEndpointAsync(BankEndpoint endpoint)
        {
            await Task.Delay(150);
            endpoint.EndpointId = $"BANK-{_endpoints.Count + 1:D3}";
            _endpoints.Add(endpoint);
            return true;
        }

        public async Task<List<SettlementBatch>> GetSettlementBatchesAsync()
        {
            await Task.Delay(100);
            return _batches;
        }

        public async Task<string> TriggerSettlementAsync(SettlementBatch batch)
        {
            await Task.Delay(500);
            batch.BatchId = $"BATCH-{DateTime.Now.Year}-{_batches.Count + 1:D3}";
            batch.Status = "InProgress";
            batch.ProcessedTime = DateTime.Now;
            _batches.Add(batch);
            return batch.BatchId;
        }

        public async Task<bool> RollbackSettlementAsync(string batchId)
        {
            await Task.Delay(300);
            var batch = _batches.FirstOrDefault(b => b.BatchId == batchId);
            if (batch != null)
            {
                batch.Status = "RolledBack";
                return true;
            }
            return false;
        }

        public async Task<bool> PushSwiftFileAsync(string batchId)
        {
            await Task.Delay(400);
            return true;
        }

        public async Task<List<PaymentTransaction>> GetTransactionsAsync(string batchId)
        {
            await Task.Delay(100);
            return _transactions.Where(t => t.BatchId == batchId).ToList();
        }

        public async Task<List<FxConversionRule>> GetFxRulesAsync()
        {
            await Task.Delay(100);
            return _fxRules;
        }

        public async Task<bool> UpdateFxRuleAsync(FxConversionRule rule)
        {
            await Task.Delay(150);
            var existing = _fxRules.FirstOrDefault(r => r.RuleId == rule.RuleId);
            if (existing != null)
            {
                _fxRules.Remove(existing);
                _fxRules.Add(rule);
                return true;
            }
            return false;
        }
    }

    public class MockSimulationService : ISimulationService
    {
        private List<SimulationScenario> _scenarios = new();
        private List<MarketDataSnapshot> _snapshots = new();
        private List<SimulationResult> _results = new();

        public MockSimulationService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _scenarios = new List<SimulationScenario>
            {
                new SimulationScenario
                {
                    ScenarioId = "SIM-001",
                    ScenarioName = "High Volume Stress Test",
                    ScenarioType = "StressTest",
                    StartDate = DateTime.Now.AddDays(-1),
                    EndDate = DateTime.Now,
                    Instruments = new List<string> { "COPPER-JAN24", "EMERALD-FEB24" },
                    Parameters = new Dictionary<string, string>
                    {
                        { "OrdersPerSecond", "10000" },
                        { "ParticipantCount", "500" }
                    },
                    Status = "Completed",
                    ExecutedTime = DateTime.Now.AddHours(-2),
                    ExecutedBy = "risk@zmx.com"
                },
                new SimulationScenario
                {
                    ScenarioId = "SIM-002",
                    ScenarioName = "New Tick Size Rule Test",
                    ScenarioType = "RuleTesting",
                    StartDate = DateTime.Now.AddDays(-7),
                    EndDate = DateTime.Now.AddDays(-6),
                    Instruments = new List<string> { "COPPER-JAN24" },
                    Parameters = new Dictionary<string, string>
                    {
                        { "NewTickSize", "15" },
                        { "OrderCount", "1000" }
                    },
                    Status = "Draft",
                    ExecutedBy = string.Empty
                }
            };

            _snapshots = new List<MarketDataSnapshot>
            {
                new MarketDataSnapshot
                {
                    SnapshotId = "SNAP-001",
                    SnapshotDate = DateTime.Now.AddDays(-1),
                    Description = "Complete market data for December 2024",
                    TotalRecords = 1500000,
                    Instruments = new List<string> { "COPPER-JAN24", "COPPER-FEB24", "EMERALD-FEB24", "COBALT-MAR24" },
                    FileSizeBytes = 250000000
                },
                new MarketDataSnapshot
                {
                    SnapshotId = "SNAP-002",
                    SnapshotDate = DateTime.Now.AddDays(-7),
                    Description = "Week of November 25-30, 2024",
                    TotalRecords = 800000,
                    Instruments = new List<string> { "COPPER-JAN24", "EMERALD-FEB24" },
                    FileSizeBytes = 125000000
                }
            };

            _results = new List<SimulationResult>
            {
                new SimulationResult
                {
                    ResultId = "RES-001",
                    ScenarioId = "SIM-001",
                    TotalOrders = 50000,
                    MatchedOrders = 48500,
                    TotalVolume = 125000000.00m,
                    AverageLatency = 5.2m,
                    ErrorCount = 12,
                    PeakMemoryUsageMB = 2048,
                    PeakCpuUsagePercent = 85.5m,
                    Metrics = new Dictionary<string, decimal>
                    {
                        { "MatchRate", 97.0m },
                        { "ThroughputTPS", 9850 },
                        { "P99Latency", 12.5m }
                    },
                    CompletedTime = DateTime.Now.AddHours(-2)
                }
            };
        }

        public async Task<List<SimulationScenario>> GetScenariosAsync()
        {
            await Task.Delay(100);
            return _scenarios;
        }

        public async Task<string> CreateScenarioAsync(SimulationScenario scenario)
        {
            await Task.Delay(150);
            scenario.ScenarioId = $"SIM-{_scenarios.Count + 1:D3}";
            scenario.Status = "Draft";
            _scenarios.Add(scenario);
            return scenario.ScenarioId;
        }

        public async Task<bool> RunSimulationAsync(string scenarioId)
        {
            await Task.Delay(2000);
            var scenario = _scenarios.FirstOrDefault(s => s.ScenarioId == scenarioId);
            if (scenario != null)
            {
                scenario.Status = "Running";
                scenario.ExecutedTime = DateTime.Now;
                
                // Simulate completion
                await Task.Delay(1000);
                scenario.Status = "Completed";
                return true;
            }
            return false;
        }

        public async Task<SimulationResult> GetSimulationResultAsync(string scenarioId)
        {
            await Task.Delay(100);
            return _results.FirstOrDefault(r => r.ScenarioId == scenarioId) ?? new SimulationResult();
        }

        public async Task<List<MarketDataSnapshot>> GetMarketDataSnapshotsAsync()
        {
            await Task.Delay(100);
            return _snapshots;
        }

        public async Task<byte[]> ExportResultsAsync(string scenarioId)
        {
            await Task.Delay(200);
            var result = _results.FirstOrDefault(r => r.ScenarioId == scenarioId);
            if (result != null)
            {
                var export = $"Scenario: {result.ScenarioId}\nTotal Orders: {result.TotalOrders}\nMatched: {result.MatchedOrders}";
                return System.Text.Encoding.UTF8.GetBytes(export);
            }
            return Array.Empty<byte>();
        }

        public async Task<List<ParticipantSimulation>> GetSimulatedParticipantsAsync(string scenarioId)
        {
            await Task.Delay(100);
            return new List<ParticipantSimulation>
            {
                new ParticipantSimulation
                {
                    ParticipantId = "SIM-PART-001",
                    ParticipantName = "Simulated Aggressive Trader",
                    TradingStrategy = "Aggressive",
                    OrdersPerMinute = 100,
                    AvailableCapital = 5000000,
                    AllowedInstruments = new List<string> { "COPPER-JAN24", "EMERALD-FEB24" }
                },
                new ParticipantSimulation
                {
                    ParticipantId = "SIM-PART-002",
                    ParticipantName = "Simulated Market Maker",
                    TradingStrategy = "Passive",
                    OrdersPerMinute = 50,
                    AvailableCapital = 10000000,
                    AllowedInstruments = new List<string> { "COPPER-JAN24" }
                }
            };
        }

        public async Task<List<BacktestMetric>> GetBacktestMetricsAsync(string scenarioId)
        {
            await Task.Delay(100);
            return new List<BacktestMetric>
            {
                new BacktestMetric
                {
                    MetricName = "Average Latency",
                    Value = 5.2m,
                    Unit = "ms",
                    Threshold = "< 10ms",
                    PassedThreshold = true
                },
                new BacktestMetric
                {
                    MetricName = "Match Rate",
                    Value = 97.0m,
                    Unit = "%",
                    Threshold = "> 95%",
                    PassedThreshold = true
                },
                new BacktestMetric
                {
                    MetricName = "Error Rate",
                    Value = 0.024m,
                    Unit = "%",
                    Threshold = "< 0.1%",
                    PassedThreshold = true
                }
            };
        }
    }
}

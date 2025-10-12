using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiningTradingApp;
using Platform.Mining.Trading.Services;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// === Radzen Services ===
builder.Services.AddRadzenComponents();

// === Trader/Broker Services ===
builder.Services.AddScoped<ITradingDashboardService, MockTradingDashboardService>();
builder.Services.AddScoped<IOrderService, MockOrderService>();
builder.Services.AddScoped<IMarketDataService, MockMarketDataService>();
builder.Services.AddScoped<ITradeService, MockTradeService>();
builder.Services.AddScoped<IPositionService, MockPositionService>();
builder.Services.AddScoped<IMarginService, MockMarginService>();

// === Operator/Compliance/Admin Services ===
builder.Services.AddScoped<IMarketOperationsService, MockMarketOperationsService>();
builder.Services.AddScoped<ISurveillanceService, MockSurveillanceService>();
builder.Services.AddScoped<IClearingService, MockClearingService>();
builder.Services.AddScoped<IWarehouseService, MockWarehouseService>();
builder.Services.AddScoped<IComplianceService, MockComplianceService>();
builder.Services.AddScoped<IUserManagementService, MockUserManagementService>();
builder.Services.AddScoped<IAuditService, MockAuditService>();
builder.Services.AddScoped<ISystemMonitoringService, MockSystemMonitoringService>();
builder.Services.AddScoped<IReconciliationService, MockReconciliationService>();

// === Backend/Technical Services ===
builder.Services.AddScoped<IMatchingEngineService, MockMatchingEngineService>();
builder.Services.AddScoped<IProductDefinitionService, MockProductDefinitionService>();
builder.Services.AddScoped<ISettlementEngineService, MockSettlementEngineService>();
builder.Services.AddScoped<ISimulationService, MockSimulationService>();

await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MiningTradingApp;
using Platform.Trading.Management.Services.Interfaces;
using Platform.Trading.Management.Services.Mock;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// === Radzen Services ===
builder.Services.AddRadzenComponents();

// === Trading Management Services ===
builder.Services.AddScoped<IBuyerService, MockBuyerService>();
builder.Services.AddScoped<ISellerService, MockSellerService>();
builder.Services.AddScoped<ITradeService, MockTradeService>();
builder.Services.AddScoped<IMarginService, MockMarginService>();
builder.Services.AddScoped<IWarehouseService, MockWarehouseService>();
builder.Services.AddScoped<IWarrantService, MockWarrantService>();
builder.Services.AddScoped<ISettlementService, MockSettlementService>();
builder.Services.AddScoped<IInspectionService, MockInspectionService>();
builder.Services.AddScoped<IMonitoringService, MockMonitoringService>();
builder.Services.AddScoped<IMineralListingService, MockMineralListingService>();

await builder.Build().RunAsync();

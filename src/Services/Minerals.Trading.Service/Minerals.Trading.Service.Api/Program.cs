using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Manager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Configure Entity Framework with In-Memory Database
builder.Services.AddDbContext<TradingDbContext>(options =>
    options.UseInMemoryDatabase("MineralsTradingDb"));

// Register managers
builder.Services.AddScoped<ITradeManager, TradeManager>();
builder.Services.AddScoped<IMineralListingManager, MineralListingManager>();
builder.Services.AddScoped<IBuyerManager, BuyerManager>();
builder.Services.AddScoped<ISellerManager, SellerManager>();
builder.Services.AddScoped<IWarehouseManager, WarehouseManager>();
builder.Services.AddScoped<IWarrantManager, WarrantManager>();
builder.Services.AddScoped<ISettlementManager, SettlementManager>();
builder.Services.AddScoped<IMarginManager, MarginManager>();
builder.Services.AddScoped<IPaymentManager, PaymentManager>();
builder.Services.AddScoped<IInspectionManager, InspectionManager>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();

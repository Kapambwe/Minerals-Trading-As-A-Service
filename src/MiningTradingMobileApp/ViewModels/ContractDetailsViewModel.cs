using CommunityToolkit.Mvvm.ComponentModel;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;

namespace MiningTradingMobileApp.ViewModels;

public partial class ContractDetailsViewModel : ObservableObject
{
    private readonly ITradeService _tradeService;

    [ObservableProperty]
    private Trade? trade;

    [ObservableProperty]
    private string tradeId = string.Empty;

    public ContractDetailsViewModel(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    public async Task LoadTradeDetailsAsync()
    {
        if (!string.IsNullOrEmpty(TradeId))
        {
            Trade = await _tradeService.GetTradeByIdAsync(TradeId);
        }
    }
}
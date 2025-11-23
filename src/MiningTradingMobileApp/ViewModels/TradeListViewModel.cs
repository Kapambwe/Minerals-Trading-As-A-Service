using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;
using System.Collections.ObjectModel;

namespace MiningTradingMobileApp.ViewModels;

public partial class TradeListViewModel : ObservableObject
{
    private readonly ITradeService _tradeService;

    [ObservableProperty]
    private ObservableCollection<Trade> trades = new ObservableCollection<Trade>();

    [ObservableProperty]
    private bool isLoading;

    public TradeListViewModel(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [RelayCommand]
    public async Task LoadTradesAsync()
    {
        IsLoading = true;
        var loadedTrades = await _tradeService.GetAllTradesAsync();
        Trades.Clear();
        foreach (var trade in loadedTrades)
        {
            Trades.Add(trade);
        }
        IsLoading = false;
    }

    [RelayCommand]
    private async Task ViewTradeDetails(string tradeId)
    {
        await Shell.Current.GoToAsync($"//BuyerTrackContract?TradeId={tradeId}");
    }
}
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;
using System.Collections.ObjectModel;

namespace MiningTradingMobileApp.ViewModels;

public partial class SellerTradeListViewModel : ObservableObject
{
    private readonly ITradeService _tradeService;

    [ObservableProperty]
    private ObservableCollection<Trade> trades = new ObservableCollection<Trade>();

    [ObservableProperty]
    private bool isLoading;

    public SellerTradeListViewModel(ITradeService tradeService)
    {
        _tradeService = tradeService;
    }

    [RelayCommand]
    public async Task LoadTradesAsync()
    {
        IsLoading = true;
        var allTrades = await _tradeService.GetAllTradesAsync();
        // For demonstration, filtering by a known seller name from mock data.
        // In a real app, you'd have a service method like GetTradesBySellerIdAsync(SellerId)
        Trades.Clear();
        foreach (var trade in allTrades.Where(t => t.SellerName == "Globex Inc." || t.SellerName == "LexCorp")) // Example filtering
        {
            Trades.Add(trade);
        }
        IsLoading = false;
    }

    [RelayCommand]
    private async Task ViewTradeDetails(string tradeId)
    {
        // Navigation to a detail page for the seller trade
        // For now, just an example, actual navigation might be different
        // await Shell.Current.GoToAsync($"//SellerTradeDetails?TradeId={tradeId}");
    }
}
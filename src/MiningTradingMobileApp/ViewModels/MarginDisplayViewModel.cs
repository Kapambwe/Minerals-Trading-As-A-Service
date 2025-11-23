using CommunityToolkit.Mvvm.ComponentModel;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;
using System.Collections.ObjectModel;

namespace MiningTradingMobileApp.ViewModels;

public partial class MarginDisplayViewModel : ObservableObject
{
    private readonly IMarginService _marginService;
    private readonly IMarginRequestService _marginRequestService;

    [ObservableProperty]
    private string tradeId = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Margin> margins = new ObservableCollection<Margin>();

    [ObservableProperty]
    private ObservableCollection<MarginRequest> marginRequests = new ObservableCollection<MarginRequest>();

    [ObservableProperty]
    private bool isLoadingMargins;

    [ObservableProperty]
    private bool isLoadingMarginRequests;

    public MarginDisplayViewModel(IMarginService marginService, IMarginRequestService marginRequestService)
    {
        _marginService = marginService;
        _marginRequestService = marginRequestService;
    }

    public async Task LoadMarginDetailsAsync()
    {
        if (!string.IsNullOrEmpty(TradeId))
        {
            IsLoadingMargins = true;
            IsLoadingMarginRequests = true;

            var loadedMargins = await _marginService.GetMarginsByTradeIdAsync(TradeId);
            Margins.Clear();
            foreach (var margin in loadedMargins)
            {
                Margins.Add(margin);
            }
            IsLoadingMargins = false;

            var loadedMarginRequests = await _marginRequestService.GetMarginRequestsForTradeAsync(TradeId);
            MarginRequests.Clear();
            foreach (var request in loadedMarginRequests)
            {
                MarginRequests.Add(request);
            }
            IsLoadingMarginRequests = false;
        }
    }
}
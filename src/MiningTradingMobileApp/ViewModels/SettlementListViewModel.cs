using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;
using System.Collections.ObjectModel;

namespace MiningTradingMobileApp.ViewModels;

public partial class SettlementListViewModel : ObservableObject
{
    private readonly ISettlementService _settlementService;

    [ObservableProperty]
    private ObservableCollection<Settlement> settlements = new ObservableCollection<Settlement>();

    [ObservableProperty]
    private bool isLoading;

    public SettlementListViewModel(ISettlementService settlementService)
    {
        _settlementService = settlementService;
    }

    [RelayCommand]
    public async Task LoadSettlementsAsync()
    {
        IsLoading = true;
        var loadedSettlements = await _settlementService.GetAllSettlementsAsync();
        Settlements.Clear();
        foreach (var settlement in loadedSettlements)
        {
            Settlements.Add(settlement);
        }
        IsLoading = false;
    }

    [RelayCommand]
    private async Task ViewSettlementDetails(string settlementId)
    {
        // Navigation to a detail page for the settlement
        // For now, just an example, actual navigation might be different
        // await Shell.Current.GoToAsync($"//SellerSettlementDetails?SettlementId={settlementId}");
    }
}
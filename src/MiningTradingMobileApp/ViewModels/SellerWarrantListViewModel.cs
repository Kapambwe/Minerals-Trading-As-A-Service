using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;
using System.Collections.ObjectModel;

namespace MiningTradingMobileApp.ViewModels;

public partial class SellerWarrantListViewModel : ObservableObject
{
    private readonly IWarrantService _warrantService;

    [ObservableProperty]
    private ObservableCollection<Warrant> warrants = new ObservableCollection<Warrant>();

    [ObservableProperty]
    private bool isLoading;

    public SellerWarrantListViewModel(IWarrantService warrantService)
    {
        _warrantService = warrantService;
    }

    [RelayCommand]
    public async Task LoadWarrantsAsync()
    {
        IsLoading = true;
        var allWarrants = await _warrantService.GetAllWarrantsAsync();
        // For demonstration, filtering by a known seller name from mock data.
        // In a real app, you'd have a service method like GetWarrantsBySellerIdAsync(SellerId)
        Warrants.Clear();
        foreach (var warrant in allWarrants.Where(w => w.PreviousOwner == "Globex Inc." || w.PreviousOwner == "LexCorp")) // Example filtering
        {
            Warrants.Add(warrant);
        }
        IsLoading = false;
    }

    [RelayCommand]
    private async Task ViewWarrantDetails(string warrantId)
    {
        // Navigation to a detail page for the seller warrant
        // For now, just an example, actual navigation might be different
        // await Shell.Current.GoToAsync($"//SellerWarrantDetails?WarrantId={warrantId}");
    }
}
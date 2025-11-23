using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;
using System.Collections.ObjectModel;

namespace MiningTradingMobileApp.ViewModels;

public partial class MineralListingListViewModel : ObservableObject
{
    private readonly IMineralListingService _mineralListingService;

    [ObservableProperty]
    private ObservableCollection<MineralListing> mineralListings = new ObservableCollection<MineralListing>();

    [ObservableProperty]
    private bool isLoading;

    public MineralListingListViewModel(IMineralListingService mineralListingService)
    {
        _mineralListingService = mineralListingService;
    }

    [RelayCommand]
    public async Task LoadMineralListingsAsync()
    {
        IsLoading = true;
        var loadedListings = await _mineralListingService.GetAllMineralListingsAsync();
        MineralListings.Clear();
        foreach (var listing in loadedListings)
        {
            MineralListings.Add(listing);
        }
        IsLoading = false;
    }

    [RelayCommand]
    private async Task ViewListingDetails(string listingId)
    {
        // Navigation to a detail page for the mineral listing
        // For now, just an example, actual navigation might be different
        // await Shell.Current.GoToAsync($"//SellerMineralListingDetails?ListingId={listingId}");
    }
}
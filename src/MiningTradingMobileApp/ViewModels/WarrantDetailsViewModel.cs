using CommunityToolkit.Mvvm.ComponentModel;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;

namespace MiningTradingMobileApp.ViewModels;

public partial class WarrantDetailsViewModel : ObservableObject
{
    private readonly IWarrantService _warrantService;

    [ObservableProperty]
    private Warrant? warrant;

    [ObservableProperty]
    private string warrantId = string.Empty;

    public WarrantDetailsViewModel(IWarrantService warrantService)
    {
        _warrantService = warrantService;
    }

    public async Task LoadWarrantDetailsAsync()
    {
        if (!string.IsNullOrEmpty(WarrantId))
        {
            Warrant = await _warrantService.GetWarrantByIdAsync(WarrantId);
        }
    }
}
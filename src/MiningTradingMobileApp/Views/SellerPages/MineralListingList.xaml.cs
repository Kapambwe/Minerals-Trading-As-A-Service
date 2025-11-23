using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.SellerPages;

public partial class MineralListingList : ContentView
{
    public MineralListingList()
    {
        InitializeComponent();
        BindingContext = new MineralListingListViewModel(new Services.MockMineralListingService()); // This will be replaced by DI later
    }
}
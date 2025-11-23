using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.SellerPages;

public partial class SellerWarrantList : ContentView
{
    public SellerWarrantList()
    {
        InitializeComponent();
        BindingContext = new SellerWarrantListViewModel(new Services.MockWarrantService()); // This will be replaced by DI later
    }
}
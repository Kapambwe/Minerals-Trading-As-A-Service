using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.SellerPages;

public partial class SellerTradeList : ContentView
{
    public SellerTradeList()
    {
        InitializeComponent();
        BindingContext = new SellerTradeListViewModel(new Services.MockTradeService()); // This will be replaced by DI later
    }
}
using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class TradeList : ContentView
{
    public TradeList()
    {
        InitializeComponent();
        BindingContext = new TradeListViewModel(new Services.MockTradeService()); // This will be replaced by DI later
    }
}
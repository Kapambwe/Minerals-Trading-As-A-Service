namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class ViewMargins : ContentPage
{
    public ViewMargins()
    {
        InitializeComponent();
    }

    private void OnLoadMarginDetailsClicked(object sender, EventArgs e)
    {
        MarginDisplayControl.TradeId = TradeIdEntry.Text;
    }
}
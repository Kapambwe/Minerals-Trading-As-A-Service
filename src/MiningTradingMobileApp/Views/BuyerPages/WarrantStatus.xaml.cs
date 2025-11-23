namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class WarrantStatus : ContentPage
{
    public WarrantStatus()
    {
        InitializeComponent();
    }

    private void OnLoadWarrantDetailsClicked(object sender, EventArgs e)
    {
        WarrantDetailsControl.WarrantId = WarrantIdEntry.Text;
    }
}
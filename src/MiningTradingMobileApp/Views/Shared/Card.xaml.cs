namespace MiningTradingMobileApp.Views.Shared;

public partial class Card : Frame
{
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(Card), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (Card)bindable;
            control.TitleLabel.Text = (string)newValue;
        });

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public new static readonly BindableProperty ContentProperty =
        BindableProperty.Create(nameof(Content), typeof(View), typeof(Card), propertyChanged: (bindable, oldValue, newValue) =>
        {
            var control = (Card)bindable;
            control.ContentView.Content = (View)newValue;
        });

    public new View Content
    {
        get => (View)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public Card()
    {
        InitializeComponent();
    }
}
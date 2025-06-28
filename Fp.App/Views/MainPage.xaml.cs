namespace Fp.App.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected MainViewModel? Model => BindingContext as MainViewModel;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Model?.OnAppearing();
    }
}

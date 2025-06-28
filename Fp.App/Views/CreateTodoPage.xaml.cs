namespace Fp.App.Views;

public partial class CreateTodoPage : ContentPage
{
    public CreateTodoPage(CreateTodoViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected CreateTodoViewModel? Model => BindingContext as CreateTodoViewModel;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Model?.OnAppearing();
    }
}
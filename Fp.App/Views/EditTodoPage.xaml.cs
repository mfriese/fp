namespace Fp.App.Views;

public partial class EditTodoPage : ContentPage
{
    public EditTodoPage(EditTodoViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected EditTodoViewModel? Model => BindingContext as EditTodoViewModel;

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Model?.OnAppearing();
    }
}
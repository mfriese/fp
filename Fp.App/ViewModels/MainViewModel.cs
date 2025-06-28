using CommunityToolkit.Maui.Alerts;
using Fp.App.Models;
using Fp.App.Services;

namespace Fp.App.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public ITodoService TodoService { get; }

    public MainViewModel(ITodoService todoService)
    {
        TodoService = todoService;
        Todos = [];
    }

    public void OnAppearing()
    {
        TodoService.
            GetTodos().
            Subscribe(
                items => Todos = new(items),
                error => MainThread.BeginInvokeOnMainThread(()
                    => Toast.Make(error.Message).Show()));
    }

    [ObservableProperty]
    private ObservableCollection<TodoModel> todos;

    [RelayCommand]
    private async Task CreateTodo()
        => await Shell.Current.GoToAsync(nameof(CreateTodoPage));

    [RelayCommand]
    private async Task EditTodo(TodoModel model)
        => await Shell.Current.GoToAsync($"{nameof(EditTodoPage)}?Id={model.Id}");
}

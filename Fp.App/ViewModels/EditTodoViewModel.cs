using CommunityToolkit.Maui.Alerts;
using Fp.App.Models;
using Fp.App.Services;

namespace Fp.App.ViewModels;

[QueryProperty("Id", "Id")]
public partial class EditTodoViewModel(ITodoService todoService) : BaseViewModel
{
    public ITodoService TodoService { get; } = todoService;

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private TodoModel? model;

    public void OnAppearing()
    {
        TodoService
            .GetTodos()
            .Subscribe(
                items => Model = items.FirstOrDefault(ii => ii.Id == Id),
                error => MainThread.BeginInvokeOnMainThread(() =>
                    Toast.Make(error.Message).Show()));
    }
}

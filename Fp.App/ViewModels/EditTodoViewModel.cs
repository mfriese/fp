using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using Fp.App.Messages;
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

    [RelayCommand]
    private void Save()
    {
        if (Model is null)
        {
            _ = Toast.Make("Cannot send null object").Show();

            return;
        }

        var updateRequest = new UpdateTodoModel()
        {
            Title = Model.Header,
            Description = Model.Description,
            IsCompleted = Model.IsCompleted
        };

        TodoService
            .UpdateTodo(Model.Id, updateRequest)
            .Subscribe(
                success => NotifyAndExit(success, Model.Id));
    }

    private static void NotifyAndExit(bool success, int id)
    {
        if (success)
        {
            WeakReferenceMessenger.Default.Send(new TodoItemChangedMessage(id));
        }
        else
        {
            _ = Toast.Make("Error sending data").Show();
        }

        Shell.Current.GoToAsync("..");
    }
}

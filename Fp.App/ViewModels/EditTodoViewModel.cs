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
            .GetTodo(Id)
            .Subscribe(
                item => Model = item,
                error => _ = Toast.Make(error.Message).Show());
    }

    [RelayCommand]
    private void Save()
    {
        if (Model is not null)
            UpdateExisting(Model);
    }

    [RelayCommand]
    private void Delete()
    {
        if (Model is not null)
            DeleteExisting(Model);
    }

    private void UpdateExisting(TodoModel model)
    {
        var updateRequest = new UpdateTodoModel()
        {
            Header = model.Header,
            Description = model.Description,
            IsCompleted = model.IsCompleted
        };

        TodoService
            .UpdateTodo(model.Id, updateRequest)
            .Subscribe(success =>
            {
                if (success)
                {
                    WeakReferenceMessenger.Default.Send(new TodoItemChangedMessage(model.Id));
                }
                else
                {
                    _ = Toast.Make("Error sending data").Show();
                }

                Shell.Current.GoToAsync("..");
            });
    }

    private void DeleteExisting(TodoModel model)
    {
        TodoService
            .DeleteTodo(model.Id)
            .Subscribe(success =>
            {
                if (success)
                {
                    WeakReferenceMessenger.Default.Send(new TodoItemDeletedMessage(model.Id));
                }
                else
                {
                    _ = Toast.Make("Error deleting data").Show();
                }
                Shell.Current.GoToAsync("..");
            });
    }
}

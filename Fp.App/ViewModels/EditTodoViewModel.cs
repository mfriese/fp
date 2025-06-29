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
        if (Id == 0)
        {
            // Creating a new item
            Model = new TodoModel
            {
                Id = 0,
                Header = string.Empty,
                Description = string.Empty,
                IsCompleted = false
            };
        }
        else
        {
            TodoService
                .GetTodo(Id)
                .Subscribe(
                    item => Model = item,
                    error => _ = Toast.Make(error.Message).Show());
        }
    }

    [RelayCommand]
    private void Save()
    {
        if (Model is null)
            return;

        if (Id != 0)
        {
            UpdateExisting(Model);
        }
        else
        {
            CreateNew(Model);
        }
    }

    private void UpdateExisting(TodoModel model)
    {
        var updateRequest = new UpdateTodoModel()
        {
            Title = model.Header,
            Description = model.Description,
            IsCompleted = model.IsCompleted
        };

        TodoService
            .UpdateTodo(model.Id, updateRequest)
            .Subscribe(
                success => NotifyAndExit(success, model.Id));
    }

    private void CreateNew(TodoModel model)
    {
        var createRequest = new CreateTodoModel()
        {
            Header = model.Header,
            Description = model.Description
        };

        TodoService
            .CreateTodo(createRequest)
            .Subscribe(
                item => NotifyAndExit(item is not null, item?.Id ?? 0));
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

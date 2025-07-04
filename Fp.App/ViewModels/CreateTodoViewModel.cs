using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using Fp.App.Messages;
using Fp.App.Models;
using Fp.App.Services;

namespace Fp.App.ViewModels;

public partial class CreateTodoViewModel(ITodoService todoService) : BaseViewModel
{
    public ITodoService TodoService { get; } = todoService;

    [ObservableProperty]
    private CreateTodoModel? model;

    public void OnAppearing()
        => Model = new() { Description = "", Header = "" };

    [RelayCommand]
    private void Save()
    {
        if (Model is not null)
            CreateNew(Model);
    }

    private void CreateNew(CreateTodoModel createDto)
    {
        TodoService
            .CreateTodo(createDto)
            .Subscribe(
                item => NotifyAndExit(item is not null, item?.Id ?? 0));
    }

    private static void NotifyAndExit(bool success, int id)
    {
        if (success)
        {
            WeakReferenceMessenger.Default.Send(new TodoItemCreatedMessage(id));
        }
        else
        {
            Toast.Make("Error sending data").Show();
        }

        Shell.Current.GoToAsync("..");
    }
}

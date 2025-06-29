using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using Fp.App.Messages;
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

        WeakReferenceMessenger.Default.Register<TodoItemChangedMessage>(this, OnItemChanged);
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
        => await Shell.Current.GoToAsync($"{nameof(EditTodoPage)}?Id={0}");

    [RelayCommand]
    private async Task EditTodo(TodoModel model)
        => await Shell.Current.GoToAsync($"{nameof(EditTodoPage)}?Id={model.Id}");

    private void OnItemChanged(object s, TodoItemChangedMessage m)
    {
        TodoService.GetTodo(m.Value).Subscribe(item =>
        {
            var existingItem = Todos.FirstOrDefault(t => t.Id == item.Id);
            if (existingItem is not null)
            {
                Todos.Remove(existingItem);
            }
            Todos.Add(item);
        }, error =>
        {
            MainThread.BeginInvokeOnMainThread(() => Toast.Make(error.Message).Show());
        });
    }
}

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Messaging;
using DynamicData;
using DynamicData.Binding;
using Fp.App.Messages;
using Fp.App.Models;
using Fp.App.Services;
using System.Reactive.Linq;

namespace Fp.App.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    public ITodoService TodoService { get; }

    private readonly SourceList<TodoModel> _sourceList = new();

    public MainViewModel(ITodoService todoService)
    {
        TodoService = todoService;

        TodoService.
            GetTodos().
            Subscribe(
                items => _sourceList.AddRange(items),
                error => Toast.Make(error.Message).Show());

        _sourceList
            .Connect()
            .AutoRefreshOnObservable(_ => this
                .WhenAnyPropertyChanged
                (
                    nameof(StateFilter),
                    nameof(TitleFilter)
                )
            )
            .Filter(model => StateFilter switch
            {
                StateOption.Completed => model.IsCompleted,
                StateOption.NotCompleted => !model.IsCompleted,
                _ => true
            })
            .Filter(model => model.Header.Contains(TitleFilter))
            .Sort(SortExpressionComparer<TodoModel>.Ascending(x => x.Id))
            .Bind(out todos)
            .Subscribe();

        WeakReferenceMessenger.Default.Register<TodoItemChangedMessage>(this, OnItemChanged);
        WeakReferenceMessenger.Default.Register<TodoItemCreatedMessage>(this, OnItemCreated);
        WeakReferenceMessenger.Default.Register<TodoItemDeletedMessage>(this, OnItemDeleted);
    }

    [ObservableProperty]
    private ReadOnlyObservableCollection<TodoModel> todos;

    [ObservableProperty]
    private StateOption stateFilter = StateOption.All;

    [ObservableProperty]
    private string titleFilter = string.Empty;

    public StateOption[] StateOptions =>
    [
        StateOption.All,
        StateOption.Completed,
        StateOption.NotCompleted
    ];

    [RelayCommand]
    private async Task CreateTodo()
        => await Shell.Current.GoToAsync(nameof(CreateTodoPage));

    [RelayCommand]
    private async Task EditTodo(TodoModel model)
        => await Shell.Current.GoToAsync($"{nameof(EditTodoPage)}?Id={model.Id}");

    private void OnItemChanged(object _, TodoItemChangedMessage m)
    {
        TodoService.GetTodo(m.Value).Subscribe(item =>
        {
            var existingItem = _sourceList.Items.FirstOrDefault(t => t.Id == item.Id);

            if (existingItem is not null)
            {
                _sourceList.Remove(existingItem);
            }

            _sourceList.Add(item);

        }, error =>
        {
            Toast.Make(error.Message).Show();
        });
    }

    private void OnItemCreated(object _, TodoItemCreatedMessage m)
    {
        TodoService.GetTodo(m.Value).Subscribe(
            item => _sourceList.Add(item),
            error => Toast.Make(error.Message).Show()
        );
    }

    private void OnItemDeleted(object _, TodoItemDeletedMessage m)
    {
        var existingItem = _sourceList.Items.FirstOrDefault(t => t.Id == m.Value);

        if (existingItem is not null)
        {
            _sourceList.Remove(existingItem);
        }
    }

    public enum StateOption
    {
        All,
        Completed,
        NotCompleted
    }
}

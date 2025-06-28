using CommunityToolkit.Mvvm.Messaging.Messages;
using Fp.App.Models;

namespace Fp.App.Messages;

internal class TodoItemCreatedMessage(TodoModel value) : ValueChangedMessage<TodoModel>(value) { }
internal class TodoItemChangedMessage(TodoModel value) : ValueChangedMessage<TodoModel>(value) { }
internal class TodoItemDeletedMessage(TodoModel value) : ValueChangedMessage<TodoModel>(value) { }

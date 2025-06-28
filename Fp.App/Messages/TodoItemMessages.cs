using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Fp.App.Messages;

internal class TodoItemCreatedMessage(int id) : ValueChangedMessage<int>(id) { }
internal class TodoItemChangedMessage(int id) : ValueChangedMessage<int>(id) { }
internal class TodoItemDeletedMessage(int id) : ValueChangedMessage<int>(id) { }

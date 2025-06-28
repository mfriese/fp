using CommunityToolkit.Maui;
using Fp.App.Api;
using Fp.App.Services;
using Refit;

namespace Fp.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.
            UseMauiApp<App>().
            ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).
            UseMauiCommunityToolkit();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.
            AddSingleton<MainViewModel>().
            AddTransient<ITodoService, TodoService>().
            AddTransientWithShellRoute<EditTodoPage, EditTodoViewModel>(nameof(EditTodoPage)).
            AddTransientWithShellRoute<CreateTodoPage, CreateTodoViewModel>(nameof(CreateTodoPage));

        // Setup Backend connection
        builder.Services
            .AddRefitClient<ITodoApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(AppSettings.ApiUrl));

        return builder.Build();
    }
}

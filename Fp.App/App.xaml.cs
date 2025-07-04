using CommunityToolkit.Mvvm.DependencyInjection;

namespace Fp.App;

public partial class App : Application
{
    public App(IServiceProvider services)
    {
        InitializeComponent();

        Ioc.Default.ConfigureServices(services);
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using netKombucha.ViewModels;
using netKombucha.Views;
using ReactiveUI;
using Splat;

namespace netKombucha;

public class App : Application, IScreen
{
    public RoutingState Router { get; } = new();

    public override void Initialize()
    {
        Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

        RegisterViews();

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow(MainWindowViewModel.GetInstance());

        base.OnFrameworkInitializationCompleted();
    }

    private void RegisterViews()
    {
        Locator.CurrentMutable.RegisterLazySingleton(() => new FileInfoView(), typeof(IViewFor<FileInfoViewModel>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new WizardView(), typeof(IViewFor<WizardViewModel>));
        Locator.CurrentMutable.RegisterLazySingleton(() => new TitleView(), typeof(IViewFor<TitleViewModel>));
    }
}
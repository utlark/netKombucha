using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using netKombucha.ViewModels;
using ReactiveUI;

namespace netKombucha.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    // ReSharper disable once MemberCanBePrivate.Global
    public MainWindow()
    {
        this.WhenActivated(_ => { });
        AvaloniaXamlLoader.Load(this);
    }

    public MainWindow(object dataContext) : this() => DataContext = dataContext;
}
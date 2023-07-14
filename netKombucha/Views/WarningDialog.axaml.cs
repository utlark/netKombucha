using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using netKombucha.ViewModels;
using ReactiveUI;

namespace netKombucha.Views;

public partial class WarningDialog : ReactiveWindow<WarningViewModel>
{
    public WarningDialog()
    {
        this.WhenActivated(_ => { });
        AvaloniaXamlLoader.Load(this);
#if DEBUG
        this.AttachDevTools();
#endif
    }

    public WarningDialog(object dataContext) : this() => DataContext = dataContext;
}
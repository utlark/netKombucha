using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace netKombucha.Views;

public partial class WarningDialog : Window
{
    public WarningDialog()
    {
        AvaloniaXamlLoader.Load(this);
#if DEBUG
        this.AttachDevTools();
#endif
    }
}
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace netKombucha.Views;

public partial class WizardView : UserControl
{
    public WizardView() => InitializeComponent();

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace netKombucha.Views;

public partial class TitleView : UserControl
{
    public TitleView() => InitializeComponent();

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
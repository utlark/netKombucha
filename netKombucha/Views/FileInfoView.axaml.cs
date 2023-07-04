using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace netKombucha.Views;

public partial class FileInfoView : UserControl
{
    public FileInfoView() => InitializeComponent();

    private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
}
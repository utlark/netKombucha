using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using netKombucha.ViewModels;
using ReactiveUI;

namespace netKombucha.Views;

public partial class FileInfoView : ReactiveUserControl<FileInfoViewModel>
{
    public FileInfoView()
    {
        this.WhenActivated(_ => { });
        AvaloniaXamlLoader.Load(this);
    }
}
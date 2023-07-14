using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using netKombucha.ViewModels;
using ReactiveUI;

namespace netKombucha.Views;

public partial class TitleView : ReactiveUserControl<TitleViewModel>
{
    public TitleView()
    {
        this.WhenActivated(_ => { });
        AvaloniaXamlLoader.Load(this);
    }
}
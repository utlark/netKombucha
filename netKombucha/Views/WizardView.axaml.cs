using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using netKombucha.ViewModels;
using ReactiveUI;

namespace netKombucha.Views;

public partial class WizardView : ReactiveUserControl<WizardViewModel>
{
    public WizardView()
    {
        this.WhenActivated(_ => { });
        AvaloniaXamlLoader.Load(this);
    }
}
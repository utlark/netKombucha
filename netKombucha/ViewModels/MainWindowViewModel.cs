using System;
using ReactiveUI;
using Splat;

namespace netKombucha.ViewModels;

public class MainWindowViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..8];
    public IScreen HostScreen { get; }

    public MainWindowViewModel(IScreen hostScreen = null)
    {
        HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();

        WizardViewModel = new WizardViewModel();
        TitleViewModel = new TitleViewModel();

        HostScreen.Router.Navigate.Execute(WizardViewModel);
    }

    public WizardViewModel WizardViewModel { get; }

    public TitleViewModel TitleViewModel { get; }
}
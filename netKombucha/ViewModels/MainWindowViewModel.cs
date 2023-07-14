using ReactiveUI;

namespace netKombucha.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    private MainWindowViewModel(IScreen hostScreen = null) : base(hostScreen)
    {
        WizardViewModel = WizardViewModel.GetInstance();
        TitleViewModel = TitleViewModel.GetInstance();

        HostScreen.Router.Navigate.Execute(WizardViewModel);
    }

    public WizardViewModel WizardViewModel { get; }

    public TitleViewModel TitleViewModel { get; }

    public static MainWindowViewModel GetInstance(IScreen hostScreen = null) => _instance ??= new MainWindowViewModel(hostScreen);

    private static MainWindowViewModel _instance;
}
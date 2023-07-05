using ReactiveUI;

namespace netKombucha.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private MainWindowViewModel()
    {
        Content = WizardViewModel = WizardViewModel.GetInstance();
    }

    public ViewModelBase Content
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public WizardViewModel WizardViewModel { get; }

    public static MainWindowViewModel GetInstance() => _instance ??= new MainWindowViewModel();

    private static MainWindowViewModel _instance;

    private ViewModelBase _content;
}
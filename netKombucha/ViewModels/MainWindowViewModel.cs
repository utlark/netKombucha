using netKombucha.Models;
using ReactiveUI;

namespace netKombucha.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private MainWindowViewModel()
    {
        MainViewModel = MainViewModel.GetInstance(this);
    }

    public MainViewModel MainViewModel { get; }

    public ProgressStage CurrentStage
    {
        get => _currentStage;
        set
        {
            this.RaiseAndSetIfChanged(ref _currentStage, value);
            MainViewModel.RaisePropertyChanged(nameof(MainViewModel.IsFirstStepActive));
            MainViewModel.RaisePropertyChanged(nameof(MainViewModel.IsSecondStepActive));
            MainViewModel.RaisePropertyChanged(nameof(MainViewModel.IsThirdStepActive));
        }
    }

    public static MainWindowViewModel GetInstance() => _instance ??= new MainWindowViewModel();

    private static MainWindowViewModel _instance;

    private ProgressStage _currentStage = ProgressStage.FileSelection;
}
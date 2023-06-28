using netKombucha.Models;
using ReactiveUI;

namespace netKombucha.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        MainViewModel = new MainViewModel(this);
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

    private ProgressStage _currentStage = ProgressStage.FileSelection;
}
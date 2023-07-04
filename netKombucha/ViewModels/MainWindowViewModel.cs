using ReactiveUI;

// ReSharper disable MemberCanBePrivate.Global

namespace netKombucha.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private MainWindowViewModel()
    {
        Content = MainViewModel = MainViewModel.GetInstance();
    }

    public ViewModelBase Content
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    public MainViewModel MainViewModel { get; }

    public static MainWindowViewModel GetInstance() => _instance ??= new MainWindowViewModel();

    private static MainWindowViewModel _instance;

    private ViewModelBase _content;
}
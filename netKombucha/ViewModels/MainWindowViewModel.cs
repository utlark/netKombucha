using ReactiveUI;

namespace netKombucha.ViewModels;

public class MainWindowViewModel : ReactiveObject
{
    public string Greeting => "Welcome to Avalonia!";
}
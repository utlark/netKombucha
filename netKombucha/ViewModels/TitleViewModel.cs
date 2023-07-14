using ReactiveUI;

namespace netKombucha.ViewModels;

public class TitleViewModel : BaseViewModel
{
    private TitleViewModel(IScreen hostScreen = null) : base(hostScreen)
    {
    }

    public static TitleViewModel GetInstance(IScreen hostScreen = null) => _instance ??= new TitleViewModel(hostScreen);

    private static TitleViewModel _instance;
}
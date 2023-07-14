using System;
using ReactiveUI;
using Splat;

namespace netKombucha.ViewModels;

public class TitleViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..8];
    public IScreen HostScreen { get; }

    public TitleViewModel(IScreen hostScreen = null)
    {
        HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
    }
}
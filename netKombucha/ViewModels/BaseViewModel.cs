using System;
using ReactiveUI;
using Splat;

namespace netKombucha.ViewModels;

public abstract class BaseViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..8];
    public IScreen HostScreen { get; }

    protected BaseViewModel(IScreen hostScreen = null) => HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
}
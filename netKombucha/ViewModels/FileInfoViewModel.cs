using System;
using System.Reactive;
using Avalonia.Platform.Storage;
using ReactiveUI;
using Splat;

namespace netKombucha.ViewModels;

public class FileInfoViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..8];

    public FileInfoViewModel(IStorageFile file, IScreen hostScreen = null)
    {
        File = file;
        HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
    }

    public IStorageFile File
    {
        get => _file;
        private set => this.RaiseAndSetIfChanged(ref _file, value);
    }

    public ReactiveCommand<Unit, IRoutableViewModel> Close => HostScreen.Router.NavigateBack;

    private IStorageFile _file;
}
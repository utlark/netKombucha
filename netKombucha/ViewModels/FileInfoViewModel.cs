using System.Reactive;
using Avalonia.Platform.Storage;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace netKombucha.ViewModels;

public class FileInfoViewModel : BaseViewModel
{
    public FileInfoViewModel(IStorageFile storageFile, IScreen hostScreen = null) : base(hostScreen)
    {
        StorageFile = storageFile;
    }

    [Reactive] public IStorageFile StorageFile { get; private set; }

    public ReactiveCommand<Unit, IRoutableViewModel> NavigateBack => HostScreen.Router.NavigateBack;
}
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using ReactiveUI;

namespace netKombucha.ViewModels;

public class FileInfoViewModel : ViewModelBase
{
    public FileInfoViewModel(IStorageFile file)
    {
        File = file;
    }

    public IStorageFile File
    {
        get => _file;
        private set => this.RaiseAndSetIfChanged(ref _file, value);
    }

    public Task Close()
    {
        var mainWindow = MainWindowViewModel.GetInstance();
        mainWindow.Content = mainWindow.MainViewModel;
        return Task.CompletedTask;
    }

    private IStorageFile _file;
}
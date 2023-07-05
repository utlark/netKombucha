using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;

namespace netKombucha.ViewModels;

public class WizardViewModel : ViewModelBase
{
    private WizardViewModel()
    {
    }

    public bool IsOpenDialogOpen
    {
        get => _isOpenDialogOpen;
        set => this.RaiseAndSetIfChanged(ref _isOpenDialogOpen, value);
    }

    public bool IsSaveDialogOpen
    {
        get => _isSaveDialogOpen;
        set => this.RaiseAndSetIfChanged(ref _isSaveDialogOpen, value);
    }

    public IStorageFile ConfigurationPackageFile
    {
        get => _configurationPackageFile;
        set => this.RaiseAndSetIfChanged(ref _configurationPackageFile, value);
    }

    public static WizardViewModel GetInstance() => _instance ??= new WizardViewModel();

    public async Task SaveFile()
    {
        IsSaveDialogOpen = true;
        ConfigurationPackageFile = await _window.StorageProvider.SaveFilePickerAsync(_filePickerSaveOptions);
        IsSaveDialogOpen = false;
    }

    public async Task OpenFile()
    {
        IsOpenDialogOpen = true;
        ConfigurationPackageFile = (await _window.StorageProvider.OpenFilePickerAsync(_filePickerOpenOptions)).FirstOrDefault();
        IsOpenDialogOpen = false;
    }

    public Task OpenFileInfo()
    {
        MainWindowViewModel.GetInstance().Content = new FileInfoViewModel(ConfigurationPackageFile);
        return Task.CompletedTask;
    }

    public Task RemoveChose()
    {
        ConfigurationPackageFile = null;
        return Task.CompletedTask;
    }

    public Task CameraSetup()
    {
        return Task.CompletedTask;
    }

    private static WizardViewModel _instance;

    private static readonly IReadOnlyList<FilePickerFileType> FilePickerFileType = new[]
    {
        new FilePickerFileType(".scp") { Patterns = new[] { "*.scp" } },
        new FilePickerFileType(".zip") { Patterns = new[] { "*.zip" } },
        new FilePickerFileType(".png") { Patterns = new[] { "*.png" } }
    };

    private bool _isOpenDialogOpen;
    private bool _isSaveDialogOpen;
    private IStorageFile _configurationPackageFile;

    private readonly Window _window = new();

    private readonly FilePickerOpenOptions _filePickerOpenOptions = new()
    {
        AllowMultiple = false,
        FileTypeFilter = FilePickerFileType
    };

    private readonly FilePickerSaveOptions _filePickerSaveOptions = new()
    {
        SuggestedFileName = "sautCameraPackage",
        DefaultExtension = FilePickerFileType.First().Name,
        FileTypeChoices = FilePickerFileType,
        ShowOverwritePrompt = true
    };
}
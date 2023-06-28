using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using netKombucha.Models;
using ReactiveUI;

namespace netKombucha.ViewModels;

public class MainViewModel : ViewModelBase
{
    public MainViewModel(MainWindowViewModel windowViewModel)
    {
        _windowViewModel = windowViewModel;
    }

    public bool IsFirstStepActive => (int)_windowViewModel.CurrentStage == 0;

    public bool IsSecondStepActive => (int)_windowViewModel.CurrentStage >= 1;

    public bool IsThirdStepActive => (int)_windowViewModel.CurrentStage >= 2;

    public bool IsOpenDialogOpen
    {
        get => _isOpenDialogOpen;
        set
        {
            this.RaiseAndSetIfChanged(ref _isOpenDialogOpen, value);
            this.RaisePropertyChanged(nameof(IsSomeDialogOpen));
        }
    }

    public bool IsSaveDialogOpen
    {
        get => _isSaveDialogOpen;
        set
        {
            this.RaiseAndSetIfChanged(ref _isSaveDialogOpen, value);
            this.RaisePropertyChanged(nameof(IsSomeDialogOpen));
        }
    }

    public bool IsSomeDialogOpen => IsSaveDialogOpen || IsOpenDialogOpen;

    public IStorageFile ConfigurationPackageFile
    {
        get => _configurationPackageFile;
        set
        {
            this.RaiseAndSetIfChanged(ref _configurationPackageFile, value);
            this.RaisePropertyChanged(nameof(ConfigurationPackageFileShortName));
            this.RaisePropertyChanged(nameof(ConfigurationPackageFileFullName));
        }
    }

    public string ConfigurationPackageFileFullName => ConfigurationPackageFile?.Name;
    public string ConfigurationPackageFileShortName => ShortName(ConfigurationPackageFile?.Name);

    public async Task SaveFile()
    {
        IsSaveDialogOpen = true;
        var result = await _window.StorageProvider.SaveFilePickerAsync(_filePickerSaveOptions);
        if (result != null)
        {
            ConfigurationPackageFile = result;
            _windowViewModel.CurrentStage = ProgressStage.CamerasSetup;
        }

        IsSaveDialogOpen = false;
    }

    public async Task OpenFile()
    {
        IsOpenDialogOpen = true;
        var result = await _window.StorageProvider.OpenFilePickerAsync(_filePickerOpenOptions);
        if (result.Count > 0)
        {
            ConfigurationPackageFile = result.First();
            _windowViewModel.CurrentStage = ProgressStage.CamerasSetup;
        }

        IsOpenDialogOpen = false;
    }

    public Task RemoveChose()
    {
        ConfigurationPackageFile = null;
        _windowViewModel.CurrentStage = ProgressStage.FileSelection;
        return Task.CompletedTask;
    }

    public Task CameraSetup()
    {
        if (ConfigurationPackageFile != null && _windowViewModel.CurrentStage == ProgressStage.CamerasSetup)
            _windowViewModel.CurrentStage = ProgressStage.Firmware;
        return Task.CompletedTask;
    }

    private static readonly IReadOnlyList<FilePickerFileType> FilePickerFileType = new[]
    {
        new FilePickerFileType(".ccp") { Patterns = new[] { "*.ccp" } },
        new FilePickerFileType(".zip") { Patterns = new[] { "*.zip" } },
        new FilePickerFileType(".png") { Patterns = new[] { "*.png" } }
    };

    private readonly MainWindowViewModel _windowViewModel;

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
        FileTypeChoices = FilePickerFileType,
        ShowOverwritePrompt = true
    };

    private static string ShortName(string fileName)
    {
        if (string.IsNullOrEmpty(fileName) || fileName.Length <= 21)
            return fileName;
        return !string.IsNullOrEmpty(fileName) ? $"{fileName[..9]}...{fileName[^9..]}" : fileName;
    }
}
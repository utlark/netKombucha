using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;
using Splat;

namespace netKombucha.ViewModels;

public class WizardViewModel : ReactiveObject, IRoutableViewModel
{
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString()[..8];
    public IScreen HostScreen { get; }

    public WizardViewModel(IScreen hostScreen = null)
    {
        HostScreen = hostScreen ?? Locator.Current.GetService<IScreen>();
        OpenFileInfo = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new FileInfoViewModel(ConfigurationPackageFile)));
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

    public ReactiveCommand<Unit, IRoutableViewModel> OpenFileInfo { get; }

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

    public Task RemoveChose()
    {
        ConfigurationPackageFile = null;
        return Task.CompletedTask;
    }

    public Task CameraSetup()
    {
        return Task.CompletedTask;
    }

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
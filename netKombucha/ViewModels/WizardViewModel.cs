using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace netKombucha.ViewModels;

public class WizardViewModel : BaseViewModel
{
    private WizardViewModel(IScreen hostScreen = null) : base(hostScreen)
    {
        OpenFile = ReactiveCommand.CreateFromTask(async () => ConfigurationFile = (await _window.StorageProvider.OpenFilePickerAsync(_fileOpenOptions)).FirstOrDefault());
        OpenFile.IsExecuting.ToPropertyEx(this, x => x.IsOpenDialogOpen);

        SaveFile = ReactiveCommand.CreateFromTask(async () => ConfigurationFile = await _window.StorageProvider.SaveFilePickerAsync(_fileSaveOptions));
        SaveFile.IsExecuting.ToPropertyEx(this, x => x.IsSaveDialogOpen);

        this.WhenAnyValue(vm => vm.IsOpenDialogOpen, vm => vm.IsSaveDialogOpen, (open, save) => open | save)
            .ToPropertyEx(this, x => x.IsSomeDialogOpen);

        OpenFileInfo = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new FileInfoViewModel(ConfigurationFile)));

        RemoveChose = ReactiveCommand.Create(() => { ConfigurationFile = null; });
        CameraSetup = ReactiveCommand.Create(() => { });
    }

    public extern bool IsOpenDialogOpen { [ObservableAsProperty] get; }

    public extern bool IsSaveDialogOpen { [ObservableAsProperty] get; }

    public extern bool IsSomeDialogOpen { [ObservableAsProperty] get; }

    [Reactive] public IStorageFile ConfigurationFile { get; set; }

    public ReactiveCommand<Unit, IRoutableViewModel> OpenFileInfo { get; }

    public ReactiveCommand<Unit, IStorageFile> SaveFile { get; }

    public ReactiveCommand<Unit, IStorageFile> OpenFile { get; }

    public ReactiveCommand<Unit, Unit> RemoveChose { get; }

    public ReactiveCommand<Unit, Unit> CameraSetup { get; }

    public static WizardViewModel GetInstance(IScreen hostScreen = null) => _instance ??= new WizardViewModel(hostScreen);

    private static WizardViewModel _instance;

    private static readonly IReadOnlyList<FilePickerFileType> FileType = new[]
    {
        new FilePickerFileType(".png") { Patterns = new[] { "*.png" } },
        new FilePickerFileType(".scp") { Patterns = new[] { "*.scp" } },
        new FilePickerFileType(".zip") { Patterns = new[] { "*.zip" } }
    };

    private readonly Window _window = new();

    private readonly FilePickerOpenOptions _fileOpenOptions = new()
    {
        AllowMultiple = false,
        FileTypeFilter = FileType
    };

    private readonly FilePickerSaveOptions _fileSaveOptions = new()
    {
        SuggestedFileName = "sautCameraPackage",
        DefaultExtension = FileType.First().Name,
        FileTypeChoices = FileType,
        ShowOverwritePrompt = true
    };
}
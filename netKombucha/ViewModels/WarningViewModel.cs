using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using netKombucha.Views;
using ReactiveUI;

namespace netKombucha.ViewModels;

public class WarningViewModel : ReactiveObject
{
    public WarningViewModel(string title, string warningText)
    {
        Title = title;
        WarningText = warningText;

        _warningDialog = new WarningDialog();
        _warningDialog.DataContext = this;

        Show = ReactiveCommand.CreateFromTask(async () =>
        {
            if (Application.Current != null && Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: not null } desktop)
            {
                var xCorrection = -1;
                var yCorrection = -1;
                _warningDialog.PositionChanged += async (_, _) =>
                {
                    if (yCorrection == -1)
                    {
                        await Task.Delay(100);
                        yCorrection = Math.Abs(_warningDialog.Position.Y - desktop.MainWindow.Position.Y);
                        xCorrection = Math.Abs(_warningDialog.Position.X - desktop.MainWindow.Position.X);
                    }

                    desktop.MainWindow.Position = new PixelPoint(_warningDialog.Position.X - xCorrection, _warningDialog.Position.Y - yCorrection);
                };
                await _warningDialog.ShowDialog(desktop.MainWindow);
            }
        });

        Close = ReactiveCommand.Create(() => _warningDialog.Close());
    }

    public string Title { get; }

    public string WarningText { get; }

    public ReactiveCommand<Unit, Unit> Show { get; }

    public ReactiveCommand<Unit, Unit> Close { get; }

    private readonly Window _warningDialog;
}
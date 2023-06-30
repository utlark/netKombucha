using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using netKombucha.Views;

namespace netKombucha.ViewModels;

public class WarningViewModel : ViewModelBase
{
    public WarningViewModel(string warningText)
    {
        WarningText = warningText;
        _warningDialog = new WarningDialog();
        _warningDialog.DataContext = this;
    }

    public string WarningText { get; }

    public async Task Show()
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
    }

    public Task OkButton()
    {
        _warningDialog.Close();
        return Task.CompletedTask;
    }

    private readonly Window _warningDialog;
}
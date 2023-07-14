using System;
using Avalonia;
using Avalonia.Media;
using Avalonia.ReactiveUI;

namespace netKombucha;

internal class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UseSkia()
            .LogToTrace()
            .UseReactiveUI()
            .UsePlatformDetect()
            .With(new FontManagerOptions
            {
                DefaultFamilyName = "avares://netKombucha/Assets/Fonts/NotoSans-Regular.ttf#Noto Sans"
            });
}
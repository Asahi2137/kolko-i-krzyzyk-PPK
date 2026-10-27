using Avalonia;
using System;

namespace zad2;

class Program {
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    // skrocone definicje metod => robi za odpowiednik klamer czyta 1 linijke
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
}

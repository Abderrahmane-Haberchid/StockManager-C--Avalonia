using Avalonia;
using System;
using StockManager.dbConfig;
using StockManager.ViewModels;

namespace StockManager;

sealed class Program : ViewModelBase
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        using (var contect = new AppDbContext())
        {
            contect.Database.EnsureCreated();
        }
        BuildAvaloniaApp()
          .StartWithClassicDesktopLifetime(args);  
    } 

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
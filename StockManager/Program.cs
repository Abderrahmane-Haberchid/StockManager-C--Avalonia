using Avalonia;
using System;
using IronPdf;
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
        License.LicenseKey = "IRONSUITE.JHONSNOWSTARK2.GMAIL.COM.2546-8F04DC89C4-E3A5FM74Z6SJNY-MG47ZI6L5DMY-YGHXHTLKXH5L-YK3NZLURUAL3-DIEXA3LBMN5W-WL7QDPN2BMRJ-F7KCBB-TV7VRHRQIJGPUA-DEPLOYMENT.TRIAL-XUJ24D.TRIAL.EXPIRES.25.JUN.2025";
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
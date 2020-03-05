using InteractiveSeven.Core;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.Workloads;
using InteractiveSeven.Startup;
using InteractiveSeven.Theming;
using MahApps.Metro;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using InteractiveSeven.Core.Data;
using Serilog.Extensions.Logging;
using System.Windows.Media;
using Tseng;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WorkloadCoordinator _workloadCoordinator;

        private IWebHost _host;
        private TsengMonitor _tsengMonitor;
        private ILogger<App> _logger;

        private static void InitializeSettings(ILogger logger)
        {
            new SettingsStore().EnsureExists(ex => logger.LogError(ex, "Error loading settings from JSON."));
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.File("logs\\i7log.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                var logger = new SerilogLoggerProvider(Log.Logger).CreateLogger(nameof(SettingsStore));
                InitializeSettings(logger);

                var uri = new UriBuilder("http", "localhost", ApplicationSettings.Instance.TsengSettings.PortNumber).Uri;
                _host = WebHost.CreateDefaultBuilder(e.Args)
                    .UseStartup<InteractiveSeven.Web.Startup>()
                    .UseUrls(uri.AbsoluteUri)
                    .ConfigureServices(DependencyRegistrar.ConfigureServices)
                    .Build();

                if (e.Args.Contains("--7h"))
                {
                    (this._host.Services.GetService<IModded>() as Modded)?.SetLoadedBy7H(true);
                }

                _logger = _host.Services.GetService<ILogger<App>>();

                _logger.LogInformation("Starting Web Host...");

                _host.Start();

                var dataLoader = _host.Services.GetService<DataLoader>();
                _logger.LogInformation("Starting Elena DataLoader...");
                dataLoader.LoadPreviousData();

                _workloadCoordinator = _host.Services.GetService<WorkloadCoordinator>();

                _tsengMonitor = _host.Services.GetService<TsengMonitor>();

                _logger.LogInformation("Starting Tseng Background Monitoring...");
                Task.Run(() => _tsengMonitor.Start()).RunInBackgroundSafely(false, LogTsengError);

                _logger.LogInformation("Initializing Theming...");
                InitializeTheming();

                _logger.LogInformation("Showing App Main Window...");
                _host.Services.GetRequiredService<MainWindow>().Show();
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Error Loading Application");
            }
        }

        private static void InitializeTheming()
        {
            try
            {
                ThemeManager.AddTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/DarkAccent1.xaml"));
                ThemeManager.AddTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/DarkAccent2.xaml"));
                ThemeManager.AddTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/LightAccent1.xaml"));
                ThemeManager.AddTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/LightAccent2.xaml"));

                ThemeManager.IsAutomaticWindowsAppModeSettingSyncEnabled = true;
                ThemeManager.SyncThemeWithWindowsAppModeSetting();

                // create custom accents
                ThemeManagerHelper.CreateTheme("Dark", Colors.Red, "CustomAccentDarkRed");
                ThemeManagerHelper.CreateTheme("Light", Colors.Red, "CustomAccentLightRed");
                ThemeManagerHelper.CreateTheme("Dark", Colors.GreenYellow);
                ThemeManagerHelper.CreateTheme("Light", Colors.GreenYellow);
                ThemeManagerHelper.CreateTheme("Dark", Colors.Indigo);
                ThemeManagerHelper.CreateTheme("Light", Colors.Indigo, changeImmediately: true);

                ThemeManager.ChangeTheme(Current, "CustomAccentDarkRed");

            }
            catch (Exception themeEx)
            {
                Log.Error(themeEx, "Error Theming Application");
            }
        }

        private void LogTsengError(Exception ex)
        {
            _logger.LogError(ex, "Error in Tseng Status Overlay.");
        }

        private async void App_OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }
}

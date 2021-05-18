using ControlzEx.Theming;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.Workloads;
using InteractiveSeven.Startup;
//using InteractiveSeven.Theming;
using MahApps.Metro;
using MahApps.Metro.Theming;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
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

        const string THEME_FILE_NAME = "i7-theme.json";
        private const string DarkBlueThemeName = "Dark.Blue";

        private IWebHost _host;
        private TsengMonitor _tsengMonitor;
        private MoodEnforcer _moodEnforcer;
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

                _moodEnforcer = _host.Services.GetService<MoodEnforcer>();

                // TODO: Don't start unless feature is "on"
                _logger.LogInformation("Starting Mood Enforcer...");
                Task.Run(() => _moodEnforcer.Start()).RunInBackgroundSafely(false, LogMoodEnforcerError);

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

        private void InitializeTheming()
        {
            try
            {
                var darkAccent1 = new Theme(new LibraryTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/DarkAccent1.xaml"), MahAppsLibraryThemeProvider.DefaultInstance));
                var darkAccent2 = new Theme(new LibraryTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/DarkAccent2.xaml"), MahAppsLibraryThemeProvider.DefaultInstance));
                var lightAccent1 = new Theme(new LibraryTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/LightAccent1.xaml"), MahAppsLibraryThemeProvider.DefaultInstance));
                var lightAccent2 = new Theme(new LibraryTheme(new Uri("pack://application:,,,/InteractiveSeven;component/Theming/CustomAccents/LightAccent2.xaml"), MahAppsLibraryThemeProvider.DefaultInstance));

                ThemeManager.Current.AddTheme(darkAccent1);
                ThemeManager.Current.AddTheme(darkAccent2);
                ThemeManager.Current.AddTheme(lightAccent1);
                ThemeManager.Current.AddTheme(lightAccent2);

                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithAppMode;

                Theme theme = LoadCurrentTheme();

                ThemeManager.Current.ChangeTheme(Current, theme);

                ThemeManager.Current.ThemeChanged += Current_ThemeChanged;

            }
            catch (Exception themeEx)
            {
                _logger.LogError(themeEx, "Error Initializing Application Theming");
            }
        }

        private Theme LoadCurrentTheme()
        {
            try
            {
                string themeName = File.ReadAllText(THEME_FILE_NAME).Trim();
                return ThemeManager.Current.Themes.First(x => x.Name == themeName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to load theme.");
            }

            return ThemeManager.Current.Themes.First(x => x.Name == DarkBlueThemeName);
        }

        private void Current_ThemeChanged(object sender, ThemeChangedEventArgs e)
        {
            try
            {
                Theme theme = ThemeManager.Current.DetectTheme(Current);
                File.WriteAllText(THEME_FILE_NAME, theme?.Name ?? DarkBlueThemeName);

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Fail to save changed theme.");
            }
        }

        private void LogTsengError(Exception ex)
        {
            _logger.LogError(ex, "Error in Tseng Status Overlay.");
        }

        private void LogMoodEnforcerError(Exception ex)
        {
            _logger.LogError(ex, "Error in Mood Enforcer.");
        }

        private async void App_OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _moodEnforcer.Stop();
            _host.Dispose();
        }
    }
}

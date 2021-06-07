using System;
using System.IO;
using System.Linq;
using System.Windows;
using ControlzEx.Theming;
using InteractiveSeven.Core.Data;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.ViewModels;
using InteractiveSeven.Startup;
using MahApps.Metro.Theming;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace InteractiveSeven
{
    public partial class App : Application
    {
        const string THEME_FILE_NAME = "theme.json";
        private const string DarkBlueThemeName = "Dark.Blue";

        private IServiceCollection _services = new ServiceCollection();
        private GameSelectWindow _gameSelectWindow;
        private AppViewModel _appViewModel = new();

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

                var loggerProvider = new SerilogLoggerProvider(Log.Logger);
                var settingsLogger = loggerProvider.CreateLogger(nameof(SettingsStore));
                InitializeSettings(settingsLogger);

                _services.AddLogging(builder => builder.AddSerilog(dispose: true));

                DependencyRegistrar.ConfigureServices(_services);

                RegisterSharedWindows(_services);

                _services.AddSingleton(_appViewModel);
                _services.AddSingleton<IModded>(_appViewModel);

                if (e.Args.Contains("--7h"))
                {
                    _appViewModel.SetLoadedBy7H(true);
                }

                Log.Logger.Information("Initializing Theming...");
                InitializeTheming();

                Log.Logger.Information("Showing Game Selection Window...");
                _gameSelectWindow = new GameSelectWindow(_appViewModel, _services);
                _gameSelectWindow.Show();
            }
            catch (Exception exception)
            {
                Log.Error(exception, "Error Loading Application");
            }
        }

        private void RegisterSharedWindows(IServiceCollection services)
        {
            services.AddSingleton<SettingsWindow>();
            services.AddSingleton<AccentStyleWindow>();
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
                Log.Logger.Error(themeEx, "Error Initializing Application Theming");
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
                Log.Logger.Error(e, "Failed to load theme.");
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
                Log.Logger.Error(exception, "Fail to save changed theme.");
            }
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            _appViewModel?.GameRunner?.Stop();
            _gameSelectWindow?.Close();
        }
    }
}

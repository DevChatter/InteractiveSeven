using InteractiveSeven.Core;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.Workloads;
using InteractiveSeven.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore;
using Tseng;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WorkloadCoordinator _workloadCoordinator;

        private IWebHost _host;
        private TsengProgram _tsengProgram;

        private static void InitializeSettings()
        {
            new SettingsStore().EnsureExists();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            InitializeSettings();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs\\i7log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var uri = new UriBuilder("http", "localhost", ApplicationSettings.Instance.TsengSettings.PortNumber).Uri;
            _host = WebHost.CreateDefaultBuilder(e.Args)
                .UseStartup<InteractiveSeven.Web.Startup>()
                .UseUrls(uri.AbsoluteUri)
                .ConfigureServices(DependencyRegistrar.ConfigureServices)
                .Build();

            _host.Start();

            var dataLoader = _host.Services.GetService<DataLoader>();
            dataLoader.LoadPreviousData();

            _workloadCoordinator = _host.Services.GetService<WorkloadCoordinator>();

            _tsengProgram = _host.Services.GetService<TsengProgram>();

            Task.Run(() => _tsengProgram.Start()).RunInBackgroundSafely(false, LogError);

            _host.Services.GetRequiredService<MainWindow>().Show();
        }

        private void LogError(Exception obj)
        {
        }

        private async void App_OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }
}

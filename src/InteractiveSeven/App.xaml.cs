using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.Workloads;
using InteractiveSeven.Startup;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Windows;
using Tseng;

namespace InteractiveSeven
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private WorkloadCoordinator _workloadCoordinator;

        private IHost _host;
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

            _host = Host.CreateDefaultBuilder(e.Args)
                .ConfigureWebHostDefaults(webHostBuilder =>
                {
                    webHostBuilder.UseStartup<InteractiveSeven.Web.Startup>();
                    //webHostBuilder.UseContentRoot()
                })
                .ConfigureServices(DependencyRegistrar.ConfigureServices)
                .Build();

            _host.Start();

            var dataLoader = _host.Services.GetService<DataLoader>();
            dataLoader.LoadPreviousData();

            _workloadCoordinator = _host.Services.GetService<WorkloadCoordinator>();

            _tsengProgram = _host.Services.GetService<TsengProgram>();
            _tsengProgram.Start();

            _host.Services.GetRequiredService<MainWindow>().Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            _host.Dispose();
        }
    }
}

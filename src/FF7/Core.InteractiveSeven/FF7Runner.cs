using System;
using System.Threading.Tasks;
using DevChatter.InteractiveGames.Core.Seven.Tseng;
using InteractiveSeven.Core;
using InteractiveSeven.Core.Moods;
using InteractiveSeven.Core.Settings;
using InteractiveSeven.Core.Workloads;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DevChatter.InteractiveGames.Core.Seven
{
    public class FF7Runner : IGameRunner
    {
        private WorkloadCoordinator _workloadCoordinator;

        private IWebHost _host;
        private TsengMonitor _tsengMonitor;
        private MoodEnforcer _moodEnforcer;
        private ServiceProvider _serviceProvider;
        private readonly ILogger<FF7Runner> _logger;

        public FF7Runner(IServiceCollection services)
        {
            services.AddFF7Registry();
            _serviceProvider = services.BuildServiceProvider();
            _logger = _serviceProvider.GetRequiredService<ILogger<FF7Runner>>();
        }

        public void Start()
        {
            try
            {
                var uri = new UriBuilder("http", "localhost", ApplicationSettings.Instance.TsengSettings.PortNumber).Uri;
                _host = WebHost.CreateDefaultBuilder()
                    .UseStartup<InteractiveSeven.Web.Startup>()
                    .UseUrls(uri.AbsoluteUri)
                    .ConfigureServices(DependencyRegistrar.ConfigureServices)
                    .Build();


                _logger.LogInformation("Starting Web Host...");

                _host.Start();

                var dataLoader = _serviceProvider.GetService<DataLoader>();
                _logger.LogInformation("Starting Elena DataLoader...");
                dataLoader.LoadPreviousData();

                _workloadCoordinator = _serviceProvider.GetService<WorkloadCoordinator>();

                _tsengMonitor = _serviceProvider.GetService<TsengMonitor>();

                _logger.LogInformation("Starting Tseng Background Monitoring...");
                Task.Run(() => _tsengMonitor.Start()).RunInBackgroundSafely(false, LogTsengError);

                _moodEnforcer = _serviceProvider.GetService<MoodEnforcer>();

                // TODO: Don't start unless feature is "on"
                _logger.LogInformation("Starting Mood Enforcer...");
                Task.Run(() => _moodEnforcer.Start()).RunInBackgroundSafely(false, LogMoodEnforcerError);

                _logger.LogInformation("Showing App Main Window...");
                _serviceProvider.GetRequiredService<MainWindow>().Show();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error Loading Application");
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

        public async void Stop()
        {
            await _host?.StopAsync();
            _moodEnforcer?.Stop();
            _host?.Dispose();
        }
    }
}

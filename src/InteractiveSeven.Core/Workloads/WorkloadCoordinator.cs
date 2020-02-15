using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace InteractiveSeven.Core.Workloads
{
    public class WorkloadCoordinator
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly ILogger<WorkloadCoordinator> _logger;
        private readonly ConcurrentQueue<IWorkload> _workloads = new ConcurrentQueue<IWorkload>();
        private bool _isRunning = false;
        private readonly object _padlock = new object();

        public WorkloadCoordinator(IMenuColorAccessor menuColorAccessor,
            ILogger<WorkloadCoordinator> logger,
            IStatusHubEmitter statusHubEmitter)
        {
            _menuColorAccessor = menuColorAccessor;
            _logger = logger;
            _statusHubEmitter = statusHubEmitter;

            DomainEvents.Register<MenuColorChanging>(HandleMenuColorChanging);
            DomainEvents.Register<RainbowModeStarted>(HandleRainbowModeStarted);
            DomainEvents.Register<MakoModeStarted>(HandleMakoModeStarted);
        }

        private void HandleMakoModeStarted(MakoModeStarted obj)
        {
            var workload = new MakoColorsWorkload(_menuColorAccessor, _statusHubEmitter, _logger);

            AddAndStart(workload);
        }

        private void HandleRainbowModeStarted(RainbowModeStarted obj)
        {
            var workload = new RainbowWorkload(_menuColorAccessor, _statusHubEmitter, _logger);

            AddAndStart(workload);
        }

        private void HandleMenuColorChanging(MenuColorChanging obj)
        {
            var workload = new MenuColorChangeWorkload(obj.MenuColors, _menuColorAccessor, _logger);

            AddAndStart(workload);
        }

        private void AddAndStart(IWorkload workload)
        {
            _workloads.Enqueue(workload);
            bool startedHere = false;

            if (!_isRunning)
            {
                lock (_padlock)
                {
                    if (!_isRunning)
                    {
                        _isRunning = true;
                        startedHere = true;
                    }
                }
            }

            if (startedHere)
            {
                Task.Run(() =>
                {
                    while (_workloads.TryDequeue(out IWorkload toRun))
                    {
                        toRun.Run();
                    }

                    _isRunning = false;
                }).RunInBackgroundSafely(false, LogWorkloadException);
            }
        }

        private void LogWorkloadException(Exception ex)
        {
            _logger.LogError(ex, "Error Running Workloads");
        }
    }
}
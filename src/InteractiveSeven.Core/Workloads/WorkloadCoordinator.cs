using System.Threading.Tasks;
using InteractiveSeven.Core.Events;
using InteractiveSeven.Core.Memory;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core.Workloads
{
    public class WorkloadCoordinator
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly ILogger<MenuColorChangeWorkload> _logger;
        // TODO: Queue of the IWorkload objects to execute one-at-a-time

        public WorkloadCoordinator(IMenuColorAccessor menuColorAccessor,
            ILogger<MenuColorChangeWorkload> logger)
        {
            _menuColorAccessor = menuColorAccessor;
            _logger = logger;

            DomainEvents.Register<MenuColorChanging>(HandleMenuColorChanging);
            DomainEvents.Register<RainbowModeStarted>(HandleRainbowModeStarted);
        }

        private void HandleRainbowModeStarted(RainbowModeStarted obj)
        {
            var workload = new RainbowWorkload(_menuColorAccessor, _logger);

            AddAndStart(workload);
        }

        private void HandleMenuColorChanging(MenuColorChanging obj)
        {
            var workload = new MenuColorChangeWorkload(obj.MenuColors, _menuColorAccessor, _logger);

            AddAndStart(workload);
        }

        private void AddAndStart(IWorkload workload)
        {
            // TODO: Method to Add and Start if not already running.
            Task.Run(workload.Run);
        }
    }
}
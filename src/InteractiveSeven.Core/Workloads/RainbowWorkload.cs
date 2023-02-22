using System;
using System.Threading.Tasks;
using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Emitters;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;

namespace InteractiveSeven.Core.Workloads
{
    public class RainbowWorkload : IWorkload
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly IStatusHubEmitter _statusHubEmitter;
        private readonly ILogger<WorkloadCoordinator> _logger;
        private ApplicationSettings Settings => ApplicationSettings.Instance;

        public RainbowWorkload(IMenuColorAccessor menuColorAccessor,
            IStatusHubEmitter statusHubEmitter,
            ILogger<WorkloadCoordinator> logger)
        {
            _menuColorAccessor = menuColorAccessor;
            _logger = logger;
            _statusHubEmitter = statusHubEmitter;
        }

        public async Task Run()
        {
            try
            {
                await _statusHubEmitter.ShowEvent("Rainbow Mode Started");

                for (int i = 0; i < Settings.MenuSettings.RainbowModeIterations; i++)
                {
                    _menuColorAccessor.SetMenuColors(ApplicationSettings.Instance.ProcessName,
                        MenuColors.RandomPalette());
                }

                await _statusHubEmitter.ShowEvent("Rainbow Mode Ended");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error changing menu colors.");
            }
        }
    }
}

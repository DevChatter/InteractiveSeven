using InteractiveSeven.Core.Diagnostics.Memory;
using InteractiveSeven.Core.Models;
using InteractiveSeven.Core.Settings;
using Microsoft.Extensions.Logging;
using System;

namespace InteractiveSeven.Core.Workloads
{
    public class RainbowWorkload : IWorkload
    {
        private readonly IMenuColorAccessor _menuColorAccessor;
        private readonly ILogger<WorkloadCoordinator> _logger;

        public RainbowWorkload(IMenuColorAccessor menuColorAccessor,
            ILogger<WorkloadCoordinator> logger)
        {
            _menuColorAccessor = menuColorAccessor;
            _logger = logger;
        }

        public void Run()
        {
            try
            {
                for (int i = 0; i < 100; i++) // TODO: Configurable Time
                {
                    _menuColorAccessor.SetMenuColors(ApplicationSettings.Instance.ProcessName,
                        MenuColors.RandomPalette());
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error changing menu colors.");
            }
        }
    }
}